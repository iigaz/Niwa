using Microsoft.EntityFrameworkCore;
using Niwa.Database;
using Niwa.Models;

namespace Niwa.Services.CollectionRepositories;

public class CollectionQueryRepository(ApplicationDbContext context) : ICollectionQueryRepository
{
    public Task<Collection?> GetNoteCollection(Guid userId, Note note)
    {
        return context.Collections.Include(collection => collection.Notes).SingleOrDefaultAsync(collection =>
            collection.UserId == userId && collection.Notes.Contains(note));
    }

    public Task<Collection?> GetById(Guid collectionId)
    {
        return context.Collections.SingleOrDefaultAsync(collection => collection.Id == collectionId);
    }

    public Task<List<Collection>> GetUserCollections(Guid userId)
    {
        return context.Collections.Where(collection => collection.UserId == userId).ToListAsync();
    }
}