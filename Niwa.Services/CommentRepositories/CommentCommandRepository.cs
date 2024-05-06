using Niwa.Database;
using Niwa.Models;

namespace Niwa.Services.CommentRepositories;

public class CommentCommandRepository(ApplicationDbContext context) : ICommentCommandRepository
{
    public async Task CreateComment(Comment comment)
    {
        comment.CreatedDateTime = DateTime.UtcNow;
        comment.UpdatedDateTime = DateTime.UtcNow;
        await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();
    }

    public async Task DeleteComment(Comment comment)
    {
        comment.Deleted = true;
        comment.UpdatedDateTime = DateTime.UtcNow;
        context.Comments.Update(comment);
        await context.SaveChangesAsync();
    }
}