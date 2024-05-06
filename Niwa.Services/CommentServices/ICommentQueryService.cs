using Niwa.Models;

namespace Niwa.Services.CommentServices;

public interface ICommentQueryService
{
    public Task<List<Comment>> GetNoteCommentsAsync(Guid noteId);
}