using Niwa.Models;

namespace Niwa.Services.NoteServices;

public interface INoteQueryService
{
    public Task<Note?> GetNoteByUsernameAndShortIdAsync(string username, string shortId);

    public Task<int> GetCommentCountAsync(Note note);

    public Task<bool> IsUserSubscribedAsync(Guid userId, Note note);
    public Task<Note?> GetNoteSnapshotAsync(Note note, Guid startingRevisionId);
    public Task<List<NoteRevision>> GetNoteRevisionsAsync(Guid noteId);
    public Task<List<Note>> GetNotesByTagAsync(Guid currentUser, string tag);
    public Task<List<Note>> GetNotesAsync(Guid currentUser);
}