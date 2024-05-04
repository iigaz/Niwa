using Microsoft.EntityFrameworkCore;
using Niwa.Models;
using Niwa.Services.UserRepositories;

namespace Niwa.Services.LoginServices;

public class LoginQueryService(IUserQueryRepository userQueryRepository) : ILoginQueryService
{
    public async Task<User?> LoginAsync(string username, string password)
    {
        var query = userQueryRepository.GetUsers().Include(user => user.Roles);
        var user = await query.SingleOrDefaultAsync(user => user.Username == username);
        return user == null || !User.CheckPassword(user.PasswordHash, password) ? null : user;
    }
}