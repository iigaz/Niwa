using Niwa.Models;

namespace Niwa.Services.GardenRepositories;

public interface IGardenQueryRepository
{
    public IQueryable<Garden> GetGardens();
    public Task<Garden?> GetByIdAsync(Guid id);
    public Task<Garden?> GetFirstByUserIdAsync(Guid id, bool withFeaturedNotes = false);
    public Task<List<Garden>> GetGardensByIdsAsync(List<Guid> ids);
    public Task<int> GetPublicNoteCountAsync(Guid id);
    public Task<List<string>> GetMostPopularTags(Guid id, int limit);
    public Task<Garden?> GetGardenWithNotes(Guid id);
}