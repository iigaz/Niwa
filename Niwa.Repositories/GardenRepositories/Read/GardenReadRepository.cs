using Microsoft.EntityFrameworkCore;
using Niwa.Database;
using Niwa.Models;
using Niwa.Models.Enums;

namespace Niwa.Repositories.GardenRepositories.Read;

public class GardenReadRepository(ApplicationDbContext context) : IGardenReadRepository
{
    public Task<Garden?> GetByIdAsync(Guid id)
    {
        return context.Gardens.SingleOrDefaultAsync(garden => garden.Id == id);
    }

    public Task<Garden?> GetFirstByUserIdAsync(Guid id)
    {
        return context.Gardens.FirstOrDefaultAsync(garden => garden.UserId == id);
    }

    public Task<List<Garden>> GetGardensByIdsAsync(List<Guid> ids)
    {
        var query = context.Gardens.Include(garden => garden.User).Where(garden => ids.Contains(garden.Id));
        return query.ToListAsync();
    }

    public async Task<int> GetPublicNoteCountAsync(Guid id)
    {
        var garden = await context.Gardens.Include(garden => garden.Notes)
            .SingleOrDefaultAsync(garden => garden.Id == id);
        if (garden == null)
            throw new NullReferenceException();
        return garden.Notes.Count(note => note.Access == Access.Public);
    }

    public Task<List<string>> GetMostPopularTags(Guid id, int limit)
    {
        return context.Gardens.Where(garden => garden.Id == id)
            .SelectMany(garden => garden.Notes)
            .SelectMany(note => note.Tags)
            .Select(tag => tag.Tag)
            .GroupBy(tag => tag)
            .Select(group => new { Tag = group.Key, Count = group.Count() })
            .OrderByDescending(whateverItIs => whateverItIs.Count)
            .Take(limit)
            .Select(whateverItIs => whateverItIs.Tag)
            .ToListAsync();
    }
}