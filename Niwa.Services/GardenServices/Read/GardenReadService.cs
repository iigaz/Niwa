using Niwa.Models;
using Niwa.Repositories.GardenRepositories.Read;
using Niwa.Repositories.UserRepositories.Read;
using Niwa.Services.Entities.GardenEntities.Read;

namespace Niwa.Services.GardenServices.Read;

public class GardenReadService(IGardenReadRepository gardenReadRepository, IUserReadRepository userReadRepository)
    : IGardenReadService
{
    public async Task<Garden?> GetFirstByUsernameAsync(string username)
    {
        var user = await userReadRepository.GetUserByUsernameAsync(username);
        if (user == null)
            return null;
        return await gardenReadRepository.GetFirstByUserIdAsync(user.Id);
    }

    public async Task<bool> IsUserSubscribedAsync(Guid userId, Garden garden)
    {
        var user = await userReadRepository.GetUserByIdAsync(userId);
        return user != null && user.SubscribedGardens.Any(g => g.Id == garden.Id);
    }

    public async Task<List<GardenWithStats>> GetGardensWithStatsByIdsAsync(List<Guid> ids)
    {
        var gardens = await gardenReadRepository.GetGardensByIdsAsync(ids);
        return (await Task.WhenAll(gardens.Select(AddStatsToGarden))).ToList();
    }

    private async Task<GardenWithStats> AddStatsToGarden(Garden garden)
    {
        var count = await gardenReadRepository.GetPublicNoteCountAsync(garden.Id);
        var popularTags = await gardenReadRepository.GetMostPopularTags(garden.Id, 5);
        var gardenWithStats = GardenWithStats.From(garden);
        gardenWithStats.PopularTags = popularTags;
        gardenWithStats.PublicNoteCount = count;
        return gardenWithStats;
    }
}