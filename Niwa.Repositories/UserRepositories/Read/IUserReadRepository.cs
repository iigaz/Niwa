using Niwa.Models;

namespace Niwa.Repositories.UserRepositories.Read;

public interface IUserReadRepository
{
    public Task<User?> LoginAsync(string username, string password, bool withRoles);

    public Task<User?> GetUserByUsernameAsync(string username, bool withRoles);

    public Task<User?> GetUserByIdAsync(Guid id);
}