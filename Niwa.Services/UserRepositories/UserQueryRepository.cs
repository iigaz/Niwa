using Microsoft.EntityFrameworkCore;
using Niwa.Database;
using Niwa.Models;

namespace Niwa.Services.UserRepositories;

public class UserQueryRepository(ApplicationDbContext context) : IUserQueryRepository
{
    public Task<User?> GetUserWithRolesAsync(string username)
    {
        return context.Users.Include(user => user.Roles).SingleOrDefaultAsync(user => user.Username == username);
    }

    public Task<User?> GetUserAsync(string username)
    {
        return context.Users.SingleOrDefaultAsync(user => user.Username == username);
    }

    public Task<User?> GetUserByIdWithSubscribedNotesAsync(Guid userId)
    {
        return context.Users.Include(user => user.SubscribedNotes).SingleOrDefaultAsync(user => user.Id == userId);
    }

    public Task<User?> GetUserByIdWithSubscribedGardensAsync(Guid userId)
    {
        return context.Users.Include(user => user.SubscribedGardens).SingleOrDefaultAsync(user => user.Id == userId);
    }

    public Task<User?> GetUserByIdWithSubscriptionsAsync(Guid userId)
    {
        return context.Users.Include(user => user.SubscribedNotes).Include(user => user.SubscribedGardens)
            .SingleOrDefaultAsync(user => user.Id == userId);
    }
}