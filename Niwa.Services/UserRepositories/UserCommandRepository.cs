using Niwa.Database;
using Niwa.Models;

namespace Niwa.Services.UserRepositories;

public class UserCommandRepository(ApplicationDbContext context) : IUserCommandRepository
{
    public async Task CreateAsync(User user)
    {
        user.CreatedDateTime = DateTime.UtcNow;
        user.UpdatedDateTime = DateTime.UtcNow;
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        user.UpdatedDateTime = DateTime.UtcNow;
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }
}