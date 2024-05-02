using Niwa.Models;

namespace Niwa.Repositories.RoleRepositories.Read;

public interface IRoleReadRepository
{
    public Task<List<Role>> GetRolesAsync();
}