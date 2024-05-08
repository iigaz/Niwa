using Niwa.Models;

namespace Niwa.Services.GardenRepositories;

public interface IGardenQueryRepository
{
    public Task<Garden?> GetGardenWithFeaturedNotesByIdAsync(Guid id);
    public Task<Garden?> GetByIdAsync(Guid id);
    public Task<Garden?> GetFirstByUserIdAsync(Guid id, bool withFeaturedNotes = false);
    public Task<List<Garden>> GetGardensByIdsAsync(List<Guid> ids);
    public Task<int> GetPublicNoteCountAsync(Guid id);
    public Task<List<string>> GetMostPopularTagsAsync(Guid id, int limit);
    public Task<Garden?> GetGardenWithNotesAsync(Guid id);
    public Task<bool> DoesFeatureNoteAsync(Note note);
}