using Niwa.Models;

namespace Niwa.Services.NoteRepositories;

public interface INoteQueryRepository
{
    public Task<Note?> GetNoteByIdAsync(Guid noteId);
    public Task<Note?> GetNoteWithFilesByIdAsync(Guid noteId);
    public Task<Note?> GetNoteWithRelevantInfoByUsernameAndShortIdAsync(string username, string shortId);
    public Task<Note?> GetNoteWithRelevantInfoByIdAsync(Guid noteId);
    public Task<int> GetCommentCountAsync(Guid noteId);
    public IQueryable<Note> GetNotesWithTagsGardenAndUsers();
    public Task<Note?> GetNoteSnapshotAsync(Note note, Guid startingRevisionId);
    public Task<List<NoteRevision>> GetNoteRevisionsAsync(Guid noteId);
    public Task<List<NoteRevision>> GetSubscribedNotesRevisionsAsync(User user, int? limit);
    public Task<List<Note>> GetSubscribedGardensNotesAsync(User user, int? limit);
    public Task<List<Note>> GetNotesByTagAsync(string tag);
    public Task<List<Note>> GetAllNotesForIndexingAsync();
}