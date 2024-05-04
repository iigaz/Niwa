using Niwa.Database;
using Niwa.Models;

namespace Niwa.Services.RoleRepositories;

public class RoleQueryRepository(ApplicationDbContext context) : IRoleQueryRepository
{
    public IQueryable<Role> GetRoles()
    {
        return context.Roles;
    }
}