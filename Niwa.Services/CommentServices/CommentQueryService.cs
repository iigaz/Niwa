using Niwa.Models;
using Niwa.Services.CommentRepositories;

namespace Niwa.Services.CommentServices;

public class CommentQueryService(ICommentQueryRepository commentQueryRepository) : ICommentQueryService
{
    public Task<List<Comment>> GetNoteCommentsAsync(Guid noteId)
    {
        return commentQueryRepository.GetNoteCommentsAsync(noteId);
    }
}