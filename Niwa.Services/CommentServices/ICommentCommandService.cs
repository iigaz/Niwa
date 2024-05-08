namespace Niwa.Services.CommentServices;

public interface ICommentCommandService
{
    public Task<bool> CreateCommentAsync(Guid userId, Guid noteId, Guid? parent, string content);
    public Task<bool> DeleteCommentAsync(Guid userId, Guid commentId);
}