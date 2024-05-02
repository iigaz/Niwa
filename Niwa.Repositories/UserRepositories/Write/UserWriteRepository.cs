using Niwa.Database;
using Niwa.Models;

namespace Niwa.Repositories.UserRepositories.Write;

public class UserWriteRepository(ApplicationDbContext context) : IUserWriteRepository
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