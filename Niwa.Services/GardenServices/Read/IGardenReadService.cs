using Niwa.Models;
using Niwa.Services.Entities.GardenEntities.Read;

namespace Niwa.Services.GardenServices.Read;

public interface IGardenReadService
{
    public Task<Garden?> GetFirstByUsernameAsync(string username);
    public Task<bool> IsUserSubscribedAsync(Guid userId, Garden garden);

    public Task<List<GardenWithStats>> GetGardensWithStatsByIdsAsync(List<Guid> ids);
}