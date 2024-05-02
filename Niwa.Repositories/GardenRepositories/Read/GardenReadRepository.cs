using Microsoft.EntityFrameworkCore;
using Niwa.Database;
using Niwa.Models;

namespace Niwa.Repositories.GardenRepositories.Read;

public class GardenReadRepository(ApplicationDbContext context) : IGardenReadRepository
{
    public Task<Garden?> GetByIdAsync(Guid id)
    {
        return context.Gardens.SingleOrDefaultAsync(garden => garden.Id == id);
    }

    public Task<Garden?> GetFirstByUserIdAsync(Guid id)
    {
        return context.Gardens.FirstOrDefaultAsync(garden => garden.UserId == id);
    }
}