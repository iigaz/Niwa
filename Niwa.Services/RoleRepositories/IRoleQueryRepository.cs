using Niwa.Models;

namespace Niwa.Services.RoleRepositories;

public interface IRoleQueryRepository
{
    public IQueryable<Role> GetRoles();
}