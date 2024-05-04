using Niwa.Models;

namespace Niwa.Services.NoteServices;

public interface INoteQueryService
{
    public Task<Note?> GetNoteByUsernameAndShortIdAsync(string username, string shortId);

    public Task<int> GetCommentCountAsync(Note note);

    public Task<bool> IsUserSubscribedAsync(Guid userId, Note note);
}