using Niwa.Models;

namespace Niwa.Services.GardenRepositories;

public interface IGardenCommandRepository
{
    public Task CreateAsync(Garden garden);
    public Task UpdateAsync(Garden garden);
}