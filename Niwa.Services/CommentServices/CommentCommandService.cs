using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using Niwa.Models;
using Niwa.Services.CommentRepositories;

namespace Niwa.Services.CommentServices;

public class CommentCommandService(
    ILogger<CommentCommandService> logger,
    ICommentCommandRepository commentCommandRepository,
    ICommentQueryRepository commentQueryRepository) : ICommentCommandService
{
    public async Task<bool> CreateCommentAsync(Guid userId, Guid noteId, Guid? parent, string content)
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
        {
            logger.LogWarning("Tried to create a comment, but could not validate it.");
            return false;
        }

        await commentCommandRepository.CreateCommentAsync(comment);
        return true;
    }

    public async Task<bool> DeleteCommentAsync(Guid userId, Guid commentId)
    {
        var comment = await commentQueryRepository.GetByIdAsync(commentId);
        if (comment == null || userId != comment.UserId)
        {
            logger.LogWarning(
                "Tried to delete a comment (Id={commentId}), but either could not find comment or user (Id={userId} was not comment author.",
                commentId, userId);
            return false;
        }

        await commentCommandRepository.DeleteCommentAsync(comment);
        return true;
    }
}