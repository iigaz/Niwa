using Niwa.Models;

namespace Niwa.Services.UserRepositories;

public interface IUserCommandRepository
{
    public Task CreateAsync(User user);

    public Task UpdateAsync(User user);
}