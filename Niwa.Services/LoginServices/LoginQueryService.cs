using Niwa.Models;
using Niwa.Services.UserRepositories;

namespace Niwa.Services.LoginServices;

public class LoginQueryService(IUserQueryRepository userQueryRepository) : ILoginQueryService
{
    public async Task<User?> LoginAsync(string username, string password)
    {
        var user = await userQueryRepository.GetUserWithRolesAsync(username);
        return user == null || !User.CheckPassword(user.PasswordHash, password) ? null : user;
    }
}