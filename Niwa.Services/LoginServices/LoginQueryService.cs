using Microsoft.EntityFrameworkCore;
using Niwa.Models;
using Niwa.Services.UserRepositories;

namespace Niwa.Services.LoginServices;

public class LoginQueryService(IUserQueryRepository userQueryRepository) : ILoginQueryService
{
    public async Task<User?> LoginAsync(string username, string password, bool withRoles = false)
    {
        var query = userQueryRepository.GetUsers();
        if (withRoles)
            query = query.Include(user => user.Roles);
        var user = await query.SingleOrDefaultAsync(user => user.Username == username);
        return user == null || !User.CheckPassword(user.PasswordHash, password) ? null : user;
    }
}