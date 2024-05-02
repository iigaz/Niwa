using Microsoft.EntityFrameworkCore;
using Niwa.Database;
using Niwa.Models;

namespace Niwa.Repositories.RoleRepositories.Read;

public class RoleReadRepository(ApplicationDbContext context) : IRoleReadRepository
{
    public async Task<List<Role>> GetRolesAsync()
    {
        return await context.Roles.ToListAsync();
    }
}