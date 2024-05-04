using Niwa.Models;

namespace Niwa.Services.UserRepositories;

public interface IUserQueryRepository
{
    public IQueryable<User> GetUsers();
}