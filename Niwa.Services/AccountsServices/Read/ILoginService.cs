using Niwa.Models;

namespace Niwa.Services.AccountsServices.Read;

public interface ILoginService
{
    public Task<User?> LoginAsync(string username, string password, bool withRoles = false);
}