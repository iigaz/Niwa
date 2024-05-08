using System.Text;
using System.Text.RegularExpressions;
using Fossil;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Niwa.Models;
using Niwa.Models.Meta;
using Niwa.Search.Services;
using Niwa.Services.GardenRepositories;
using Niwa.Services.NoteRepositories;
using Niwa.Services.NoteServices.Models;
using Sqids;

namespace Niwa.Services.NoteServices;

public class NoteCommandService(
    ILogger<NoteCommandService> logger,
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
        {
            logger.LogWarning("Tried to create note, but could not validate tags.");
            return null;
        }

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
        // It does not need to be in a transaction. The latter command just bumps the updated date on garden.
        await gardenCommandRepository.UpdateAsync(noteCommand.Garden);

        logger.LogInformation("Created note (Id={noteId}, ShortId={shortId})", noteId, shortId);
        {
            var addedNote = await noteQueryRepository.GetNoteWithRelevantInfoByIdAsync(noteId);
            if (addedNote != null)
            {
                await noteSearchCommandService.AddNoteToIndexAsync(addedNote);
                logger.LogInformation("Added note (Id={noteId}) to index.", noteId);
            }
            else
            {
                logger.LogWarning("After creating a note (Id={noteId}), could not find it in the database.", noteId);
            }
        }
        return note;
    }

    public async Task<bool> UpdateAsync(UpdateNoteCommand noteCommand)
    {
        var matches = new Regex(@"!\[.*?\]\((.*?)\)")
            .Matches(noteCommand.Content);
        var image = matches.Count > 0 ? matches[0].Groups[1].Value : null;
        var originalNote = await noteQueryRepository.GetNoteWithRelevantInfoByIdAsync(noteCommand.Id);
        if (originalNote == null)
        {
            logger.LogWarning("Tried to update note (Id={noteId}), but could not find it.", noteCommand.Id);
            return false;
        }

        var tags = noteCommand.Tags
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (tags.Any(tag =>
                tag.Length is > Lengths.TagMax or < Lengths.TagMin || !Regex.IsMatch(tag, "[-a-z0-9_\\+]+")))
        {
            logger.LogWarning("Tried to update note (Id={noteId}), but could not validate tags.", noteCommand.Id);
            return false;
        }

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

        logger.LogInformation(
            "Added note (Id={noteId}) revision. Use of compression: title={compressedTitle}, summary={compressedSummary}, content={compressedContent}",
            originalNote.Id, !rewriteTitle, !rewriteSummary, !rewriteContent);
        return true;
    }

    public async Task<bool> AddFilesAsync(Guid noteId, List<NoteFile> files)
    {
        var originalNote = await noteQueryRepository.GetNoteWithFilesByIdAsync(noteId);
        if (originalNote == null)
        {
            logger.LogWarning("Tried to add files, but could not find note (Id={noteId})", noteId);
            return false;
        }

        files.ForEach(file => { originalNote.Files.Add(file); });
        await noteCommandRepository.UpdateAsync(originalNote);
        return true;
    }

    public async Task<bool> RemoveFileAsync(Guid noteId, NoteFile noteFile)
    {
        var originalNote = await noteQueryRepository.GetNoteWithFilesByIdAsync(noteId);
        if (originalNote == null)
        {
            logger.LogWarning("Tried to remove files, but could not find note (Id={noteId})", noteId);
            return false;
        }

        originalNote.Files.Remove(noteFile);
        await noteCommandRepository.UpdateAsync(originalNote);
        logger.LogInformation("Removed note files (Id={noteId}) from database.", noteId);
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