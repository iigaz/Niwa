using Microsoft.EntityFrameworkCore;
using Niwa.Database;
using Niwa.Models;

namespace Niwa.Repositories.UserRepositories.Read;

public class UserReadRepository(ApplicationDbContext context) : IUserReadRepository
{
    public async Task<User?> LoginAsync(string username, string password, bool withRoles = false)
    {
        var user = await GetUserByUsernameAsync(username, withRoles);
        return user == null || !User.CheckPassword(user.PasswordHash, password) ? null : user;
    }

    public Task<User?> GetUserByIdAsync(Guid id, bool withSubscribedGardens = false)
    {
        IQueryable<User> query = context.Users;
        if (withSubscribedGardens)
            query = query.Include(user => user.SubscribedGardens);
        return query.SingleOrDefaultAsync(user => user.Id == id);
    }

    public Task<User?> GetUserByUsernameAsync(string username, bool withRoles = false)
    {
        IQueryable<User> query = context.Users;
        if (withRoles)
            query = query.Include(user => user.Roles);
        return query.SingleOrDefaultAsync(user => user.Username == username);
    }
}