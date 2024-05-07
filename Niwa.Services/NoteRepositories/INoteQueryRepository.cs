using Niwa.Models;

namespace Niwa.Services.NoteRepositories;

public interface INoteQueryRepository
{
    public IQueryable<Note> GetNotes();
    public Task<Note?> GetNoteSnapshotAsync(Note note, Guid startingRevisionId);
    public Task<List<NoteRevision>> GetNoteRevisionsAsync(Guid noteId);
    public Task<List<NoteRevision>> GetSubscribedNotesRevisionsAsync(User user, int? limit);
    public Task<List<Note>> GetSubscribedGardensNotesAsync(User user, int? limit);
    public Task<List<Note>> GetNotesByTagAsync(string tag);
}