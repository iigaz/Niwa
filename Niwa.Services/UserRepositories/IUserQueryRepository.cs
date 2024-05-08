using Niwa.Models;

namespace Niwa.Services.UserRepositories;

public interface IUserQueryRepository
{
    public Task<User?> GetUserWithRolesAsync(string username);

    public Task<User?> GetUserAsync(string username);

    public Task<User?> GetUserByIdWithSubscribedNotesAsync(Guid userId);
    public Task<User?> GetUserByIdWithSubscribedGardensAsync(Guid userId);
    public Task<User?> GetUserByIdWithSubscriptionsAsync(Guid userId);
}