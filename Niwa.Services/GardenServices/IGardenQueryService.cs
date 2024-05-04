using Niwa.Models;
using Niwa.Services.GardenServices.Models;

namespace Niwa.Services.GardenServices;

public interface IGardenQueryService
{
    public Task<Garden?> GetFirstByUsernameAsync(string username, bool withFeaturedNotes = false);
    public Task<bool> IsUserSubscribedAsync(Guid userId, Garden garden);
    public Task<List<GardenWithStats>> GetGardensWithStatsByIdsAsync(List<Guid> ids);
    public Task<bool> DoesFeatureNoteAsync(Note note);
}