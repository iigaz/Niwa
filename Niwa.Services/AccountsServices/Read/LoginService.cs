using Niwa.Models;
using Niwa.Repositories.UserRepositories.Read;

namespace Niwa.Services.AccountsServices.Read;

public class LoginService(IUserReadRepository userReadRepository) : ILoginService
{
    public Task<User?> LoginAsync(string username, string password, bool withRoles = false)
    {
        return userReadRepository.LoginAsync(username, password, withRoles);
    }
}