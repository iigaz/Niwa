using Niwa.Database;
using Niwa.Models;

namespace Niwa.Services.UserRepositories;

public class UserQueryRepository(ApplicationDbContext context) : IUserQueryRepository
{
    public IQueryable<User> GetUsers()
    {
        return context.Users;
    }
}