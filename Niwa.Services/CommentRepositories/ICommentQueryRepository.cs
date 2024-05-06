using Niwa.Models;

namespace Niwa.Services.CommentRepositories;

public interface ICommentQueryRepository
{
    public Task<List<Comment>> GetNoteCommentsAsync(Guid noteId);
    public Task<Comment?> GetById(Guid commentId);
}