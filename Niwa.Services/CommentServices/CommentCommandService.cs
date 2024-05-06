using System.ComponentModel.DataAnnotations;
using Niwa.Models;
using Niwa.Services.CommentRepositories;

namespace Niwa.Services.CommentServices;

public class CommentCommandService(
    ICommentCommandRepository commentCommandRepository,
    ICommentQueryRepository commentQueryRepository) : ICommentCommandService
{
    public async Task<bool> CreateComment(Guid userId, Guid noteId, Guid? parent, string content)
    {
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            NoteId = noteId,
            Content = content,
            Deleted = false,
            ParentId = parent
        };
        if (!Validator.TryValidateObject(comment, new ValidationContext(comment), new List<ValidationResult>()))
            return false;
        await commentCommandRepository.CreateComment(comment);
        return true;
    }

    public async Task<bool> DeleteComment(Guid userId, Guid commentId)
    {
        var comment = await commentQueryRepository.GetById(commentId);
        if (comment == null || userId != comment.UserId)
            return false;
        await commentCommandRepository.DeleteComment(comment);
        return true;
    }
}