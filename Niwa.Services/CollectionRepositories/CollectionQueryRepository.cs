using Microsoft.EntityFrameworkCore;
using Niwa.Database;
using Niwa.Models;

namespace Niwa.Services.CollectionRepositories;

public class CollectionQueryRepository(ApplicationDbContext context) : ICollectionQueryRepository
{
    public Task<Collection?> GetNoteCollectionAsync(Guid userId, Note note)
    {
        return context.Collections.Include(collection => collection.Notes).SingleOrDefaultAsync(collection =>
            collection.UserId == userId && collection.Notes.Contains(note));
    }

    public Task<Collection?> GetByIdAsync(Guid collectionId)
    {
        return context.Collections.Include(collection => collection.Notes).ThenInclude(note => note.Garden)
            .Include(collection => collection.Notes).ThenInclude(note => note.User)
            .Include(collection => collection.Notes).ThenInclude(note => note.Tags)
            .SingleOrDefaultAsync(collection => collection.Id == collectionId);
    }

    public Task<List<Collection>> GetUserCollectionsAsync(Guid userId)
    {
        return context.Collections.Where(collection => collection.UserId == userId).ToListAsync();
    }
}