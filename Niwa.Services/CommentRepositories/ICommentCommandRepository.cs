using Niwa.Models;

namespace Niwa.Services.CommentRepositories;

public interface ICommentCommandRepository
{
    public Task CreateCommentAsync(Comment comment);
    public Task DeleteCommentAsync(Comment comment);
}