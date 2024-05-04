using Niwa.Models;

namespace Niwa.Services.LoginServices;

public interface ILoginQueryService
{
    public Task<User?> LoginAsync(string username, string password);
}