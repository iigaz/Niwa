using System.Text;
using Fossil;
using Microsoft.EntityFrameworkCore;
using Niwa.Database;
using Niwa.Models;
using Niwa.Models.Enums;

namespace Niwa.Services.NoteRepositories;

public class NoteQueryRepository(ApplicationDbContext context) : INoteQueryRepository
{
    public IQueryable<Note> GetNotes()
    {
        return context.Notes;
    }

    public async Task<Note?> GetNoteSnapshotAsync(Note note, Guid startingRevisionId)
    {
        var revisions = await GetRevisions(startingRevisionId);
        revisions.Reverse();
        foreach (var revision in revisions)
        {
            if (revision.TitleRewritten)
                note.Title = revision.TitleDelta!;
            else if (revision.TitleDelta != null)
                note.Title = ApplyPatch(note.Title, revision.TitleDelta) ?? "";
            if (revision.SummaryRewritten)
                note.Summary = revision.SummaryDelta!;
            else if (revision.SummaryDelta != null)
                note.Summary = ApplyPatch(note.Summary, revision.SummaryDelta) ?? "";
            if (revision.ContentRewritten)
                note.Content = revision.ContentDelta!;
            else if (revision.ContentDelta != null)
                note.Content = ApplyPatch(note.Content, revision.ContentDelta) ?? "";
            if (revision.Access != null)
                note.Access = revision.Access.Value;
        }

        note.LatestRevision = revisions[^1];
        note.LatestRevisionId = revisions[^1].Id;
        return note;
    }

    public async Task<List<NoteRevision>> GetNoteRevisionsAsync(Guid noteId)
    {
        var note = await context.Notes.SingleOrDefaultAsync(note => note.Id == noteId);
        if (note == null)
            return [];
        return await GetRevisions(note.LatestRevisionId);
    }

    public Task<List<NoteRevision>> GetSubscribedNotesRevisionsAsync(User user, int? limit)
    {
        IQueryable<NoteRevision> query = context.NoteRevisions
            .Where(revision => user.SubscribedNotes.Contains(revision.Note) &&
                               (revision.Note.Access == Access.Public || revision.Note.UserId == user.Id))
            .Include(revision => revision.Note)
            .ThenInclude(note => note.User)
            .OrderByDescending(revision => revision.CreatedDateTime);
        if (limit != null)
            query = query.Take(limit.Value);
        return query.ToListAsync();
    }

    public Task<List<Note>> GetSubscribedGardensNotesAsync(User user, int? limit)
    {
        IQueryable<Note> query = context.Notes
            .Where(note => user.SubscribedGardens.Contains(note.Garden) &&
                           (note.Access == Access.Public || note.UserId == user.Id))
            .Include(note => note.Garden)
            .Include(note => note.User)
            .OrderByDescending(note => note.CreatedDateTime);
        if (limit != null)
            query = query.Take(limit.Value);
        return query.ToListAsync();
    }


    public Task<List<Note>> GetNotesByTagAsync(string tag)
    {
        return context.Notes.Include(note => note.Garden).ThenInclude(garden => garden.User).Include(note => note.Tags)
            .Where(note => note.Tags.Any(t => t.Tag == tag)).ToListAsync();
    }

    private static string? ApplyPatch(string origin, string delta)
    {
        var originBytes = Encoding.UTF8.GetBytes(origin);
        var deltaBytes = Encoding.UTF8.GetBytes(delta);
        var targetBytes = Delta.Apply(originBytes, deltaBytes);
        return targetBytes == null ? null : Encoding.UTF8.GetString(targetBytes);
    }

    private Task<List<NoteRevision>> GetRevisions(Guid startingRevisionId)
    {
        return context.NoteRevisions.FromSql($"select * from get_all_revisions({startingRevisionId})").ToListAsync();
    }
}