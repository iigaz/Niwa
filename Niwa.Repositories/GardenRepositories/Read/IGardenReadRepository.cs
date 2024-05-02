using Niwa.Models;

namespace Niwa.Repositories.GardenRepositories.Read;

public interface IGardenReadRepository
{
    public Task<Garden?> GetByIdAsync(Guid id);
    public Task<Garden?> GetFirstByUserIdAsync(Guid id);
}