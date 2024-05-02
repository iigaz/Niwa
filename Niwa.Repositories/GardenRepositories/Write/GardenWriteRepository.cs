using Niwa.Database;
using Niwa.Models;

namespace Niwa.Repositories.GardenRepositories.Write;

public class GardenWriteRepository(ApplicationDbContext context) : IGardenWriteRepository
{
    public async Task CreateAsync(Garden garden)
    {
        garden.CreatedDateTime = DateTime.UtcNow;
        garden.UpdatedDateTime = DateTime.UtcNow;
        await context.AddAsync(garden);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Garden garden)
    {
        garden.UpdatedDateTime = DateTime.UtcNow;
        context.Update(garden);
        await context.SaveChangesAsync();
    }
}