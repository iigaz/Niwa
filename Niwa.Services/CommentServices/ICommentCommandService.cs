namespace Niwa.Services.CommentServices;

public interface ICommentCommandService
{
    public Task<bool> CreateComment(Guid userId, Guid noteId, Guid? parent, string content);
    public Task<bool> DeleteComment(Guid userId, Guid commentId);
}