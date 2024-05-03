using Niwa.Models;

namespace Niwa.Repositories.GardenRepositories.Read;

public interface IGardenReadRepository
{
    public Task<Garden?> GetByIdAsync(Guid id);
    public Task<Garden?> GetFirstByUserIdAsync(Guid id);
    public Task<List<Garden>> GetGardensByIdsAsync(List<Guid> ids);
    public Task<int> GetPublicNoteCountAsync(Guid id);
    public Task<List<string>> GetMostPopularTags(Guid id, int limit);
}