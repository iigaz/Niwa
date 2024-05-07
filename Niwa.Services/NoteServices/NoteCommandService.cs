using System.Text;
using System.Text.RegularExpressions;
using Fossil;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Niwa.Models;
using Niwa.Models.Meta;
using Niwa.Search.Services;
using Niwa.Services.GardenRepositories;
using Niwa.Services.NoteRepositories;
using Niwa.Services.NoteServices.Models;
using Sqids;

namespace Niwa.Services.NoteServices;

public class NoteCommandService(
    INoteCommandRepository noteCommandRepository,
    INoteQueryRepository noteQueryRepository,
    IConfiguration configuration,
    IGardenCommandRepository gardenCommandRepository,
    INoteSearchCommandService noteSearchCommandService) : INoteCommandService
{
    public async Task<Note?> CreateAsync(CreateNoteCommand noteCommand)
    {
        var matches = new Regex(@"!\[.*?\]\((.*?)\)")
            .Matches(noteCommand.Content);
        var image = matches.Count > 0 ? matches[0].Groups[1].Value : null;
        var tags = noteCommand.Tags.Split(' ',
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (tags.Any(tag =>
                tag.Length is > Lengths.TagMax or < Lengths.TagMin || !Regex.IsMatch(tag, "[-a-z0-9_\\+]+")))
            return null;
        var createdDateTime = DateTime.UtcNow;
        var revisionId = Guid.NewGuid();
        var revision = new NoteRevision
        {
            Id = revisionId,
            TitleDelta = noteCommand.Title,
            TitleRewritten = true,
            SummaryDelta = noteCommand.Summary,
            SummaryRewritten = true,
            Access = noteCommand.Access,
            ContentDelta = noteCommand.Content,
            ContentRewritten = true,
            PreviousRevisionId = null,
            CreatedDateTime = createdDateTime
        };
        var noteId = Guid.NewGuid();
        var shortId = new SqidsEncoder<long>(new SqidsOptions
        {
            Alphabet = configuration.GetSection("Sqids:Alphabet").Get<string>() ??
                       "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        }).Encode(
            ((DateTimeOffset)createdDateTime).ToUnixTimeSeconds());
        var note = new Note
        {
            Id = noteId,
            ShortId = shortId,
            UserId = noteCommand.UserId,
            LatestRevisionId = revisionId,
            LatestRevision = revision,
            GardenId = noteCommand.Garden.Id,
            Title = noteCommand.Title,
            Summary = noteCommand.Summary,
            Content = noteCommand.Content,
            Image = image,
            Access = noteCommand.Access,
            Tags = tags.Select(tag => new NoteTag { NoteId = noteId, Tag = tag }).ToList(),
            Files = noteCommand.Files,
            CreatedDateTime = createdDateTime
        };
        await noteCommandRepository.CreateAsync(note, revision);
        await gardenCommandRepository.UpdateAsync(noteCommand.Garden);

        var addedNote = await noteQueryRepository.GetNotes()
            .Include(n => n.Garden)
            .Include(n => n.Tags)
            .Include(n => n.User)
            .Include(n => n.LatestRevision)
            .SingleOrDefaultAsync(n => n.Id == noteId);
        if (addedNote != null)
            await noteSearchCommandService.AddNoteToIndexAsync(addedNote);
        return note;
    }

    public async Task<bool> UpdateAsync(UpdateNoteCommand noteCommand)
    {
        var matches = new Regex(@"!\[.*?\]\((.*?)\)")
            .Matches(noteCommand.Content);
        var image = matches.Count > 0 ? matches[0].Groups[1].Value : null;
        var originalNote = await noteQueryRepository.GetNotes()
            .Include(note => note.Garden)
            .Include(n => n.Tags)
            .Include(n => n.User)
            .Include(n => n.LatestRevision)
            .SingleOrDefaultAsync(note => note.Id == noteCommand.Id);
        if (originalNote == null)
            throw new ArgumentNullException();

        var tags = noteCommand.Tags
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (tags.Any(tag =>
                tag.Length is > Lengths.TagMax or < Lengths.TagMin || !Regex.IsMatch(tag, "[-a-z0-9_\\+]+")))
            return false;

        var (titleDelta, rewriteTitle) = GetDelta(originalNote.Title, noteCommand.Title);
        var (summaryDelta, rewriteSummary) = GetDelta(originalNote.Summary, noteCommand.Summary);
        var (contentDelta, rewriteContent) = GetDelta(originalNote.Content, noteCommand.Content);


        var revisionId = Guid.NewGuid();
        var revision = new NoteRevision
        {
            Id = revisionId,
            TitleDelta = rewriteTitle ? noteCommand.Title : titleDelta,
            TitleRewritten = rewriteTitle,
            SummaryDelta = rewriteSummary ? noteCommand.Summary : summaryDelta,
            SummaryRewritten = rewriteSummary,
            Access = originalNote.Access == noteCommand.Access ? null : noteCommand.Access,
            ContentDelta = rewriteContent ? noteCommand.Content : contentDelta,
            ContentRewritten = rewriteContent,
            PreviousRevisionId = originalNote.LatestRevisionId,
            CreatedDateTime = DateTime.UtcNow
        };
        originalNote.Access = noteCommand.Access;
        originalNote.Title = noteCommand.Title;
        originalNote.Summary = noteCommand.Summary;
        originalNote.Content = noteCommand.Content;
        originalNote.Image = image;
        originalNote.Tags = tags.Select(tag => new NoteTag { NoteId = originalNote.Id, Tag = tag }).ToList();
        await noteCommandRepository.UpdateAsync(originalNote, revision);
        await gardenCommandRepository.UpdateAsync(originalNote.Garden);

        await noteSearchCommandService.UpdateNoteAsync(originalNote);
        return true;
    }

    public async Task<bool> AddFilesAsync(Guid noteId, List<NoteFile> files)
    {
        var originalNote = await noteQueryRepository.GetNotes().Include(note => note.Files)
            .SingleOrDefaultAsync(note => note.Id == noteId);
        if (originalNote == null)
            return false;
        files.ForEach(file => { originalNote.Files.Add(file); });
        await noteCommandRepository.UpdateAsync(originalNote);
        return true;
    }

    public async Task<bool> RemoveFileAsync(Guid noteId, NoteFile noteFile)
    {
        var originalNote = await noteQueryRepository.GetNotes().Include(note => note.Files)
            .SingleOrDefaultAsync(note => note.Id == noteId);
        if (originalNote == null)
            return false;
        originalNote.Files.Remove(noteFile);
        await noteCommandRepository.UpdateAsync(originalNote);
        return true;
    }

    private static (string?, bool) GetDelta(string s1, string s2)
    {
        if (s1 == s2)
            return (null, false);
        var originalTitle = Encoding.UTF8.GetBytes(s1);
        var targetTitle = Encoding.UTF8.GetBytes(s2);
        var titleDeltaBytes = Delta.Create(originalTitle, targetTitle);
        var titleDelta = Encoding.UTF8.GetString(titleDeltaBytes);
        var rewriteTitle = titleDelta.Length >= targetTitle.Length;
        return (titleDelta, rewriteTitle);
    }
}