using Microsoft.EntityFrameworkCore;
using Niwa.Models;
using Niwa.Services.GardenRepositories;
using Niwa.Services.GardenServices.Models;
using Niwa.Services.UserRepositories;

namespace Niwa.Services.GardenServices;

public class GardenQueryService(IGardenQueryRepository gardenQueryRepository, IUserQueryRepository userQueryRepository)
    : IGardenQueryService
{
    public async Task<Garden?> GetFirstByUsernameAsync(string username, bool withFeaturedNotes = false)
    {
        var user = await userQueryRepository.GetUsers().SingleOrDefaultAsync(user => user.Username == username);
        if (user == null)
            return null;
        return await gardenQueryRepository.GetFirstByUserIdAsync(user.Id, withFeaturedNotes);
    }

    public async Task<bool> IsUserSubscribedAsync(Guid userId, Garden garden)
    {
        var user = await userQueryRepository.GetUsers().SingleOrDefaultAsync(user => user.Id == userId);
        return user != null && user.SubscribedGardens.Any(g => g.Id == garden.Id);
    }

    public async Task<List<GardenWithStats>> GetGardensWithStatsByIdsAsync(List<Guid> ids)
    {
        var gardens = await gardenQueryRepository.GetGardensByIdsAsync(ids);
        return (await Task.WhenAll(gardens.Select(AddStatsToGarden))).ToList();
    }

    private async Task<GardenWithStats> AddStatsToGarden(Garden garden)
    {
        var count = await gardenQueryRepository.GetPublicNoteCountAsync(garden.Id);
        var popularTags = await gardenQueryRepository.GetMostPopularTags(garden.Id, 5);
        var gardenWithStats = GardenWithStats.From(garden);
        gardenWithStats.PopularTags = popularTags;
        gardenWithStats.PublicNoteCount = count;
        return gardenWithStats;
    }
}