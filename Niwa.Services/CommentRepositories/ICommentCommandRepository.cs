using Niwa.Models;

namespace Niwa.Services.CommentRepositories;

public interface ICommentCommandRepository
{
    public Task CreateComment(Comment comment);
    public Task DeleteComment(Comment comment);
}