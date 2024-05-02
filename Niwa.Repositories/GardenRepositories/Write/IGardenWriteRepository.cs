using Niwa.Models;

namespace Niwa.Repositories.GardenRepositories.Write;

public interface IGardenWriteRepository
{
    public Task CreateAsync(Garden garden);
    public Task UpdateAsync(Garden garden);
}