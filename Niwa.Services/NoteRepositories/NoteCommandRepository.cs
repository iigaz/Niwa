using Niwa.Database;
using Niwa.Models;

namespace Niwa.Services.NoteRepositories;

public class NoteCommandRepository(ApplicationDbContext context) : INoteCommandRepository
{
    public async Task CreateAsync(Note note, NoteRevision noteRevision)
    {
        note.CreatedDateTime = DateTime.UtcNow;
        noteRevision.CreatedDateTime = DateTime.UtcNow;
        note.LatestRevisionId = noteRevision.Id;
        await using var transaction = await context.Database.BeginTransactionAsync();
        await context.NoteRevisions.AddAsync(noteRevision);
        await context.Notes.AddAsync(note);
        await context.SaveChangesAsync();
        await transaction.CommitAsync();
    }

    public async Task UpdateAsync(Note note, NoteRevision noteRevision)
    {
        noteRevision.CreatedDateTime = DateTime.UtcNow;
        note.LatestRevisionId = noteRevision.Id;
        await using var transaction = await context.Database.BeginTransactionAsync();
        await context.NoteRevisions.AddAsync(noteRevision);
        context.Notes.Update(note);
        await context.SaveChangesAsync();
        await transaction.CommitAsync();
    }

    public async Task UpdateAsync(Note note)
    {
        context.Notes.Update(note);
        await context.SaveChangesAsync();
    }
}