using Niwa.Models;

namespace Niwa.Repositories.UserRepositories.Write;

public interface IUserWriteRepository
{
    public Task CreateAsync(User user);

    public Task UpdateAsync(User user);
}