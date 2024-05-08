using Microsoft.EntityFrameworkCore;
using Niwa.Database;
using Niwa.Models;

namespace Niwa.Services.CommentRepositories;

public class CommentQueryRepository(ApplicationDbContext context) : ICommentQueryRepository
{
    public Task<List<Comment>> GetNoteCommentsAsync(Guid noteId)
    {
        return context.Comments.Where(comment => !comment.Deleted && comment.NoteId == noteId)
            .Include(comment => comment.Parent).Include(comment => comment.User).Include(comment => comment.User)
            .OrderByDescending(comment => comment.CreatedDateTime)
            .ToListAsync();
    }

    public Task<Comment?> GetByIdAsync(Guid commentId)
    {
        return context.Comments.SingleOrDefaultAsync(comment => comment.Id == commentId);
    }
}