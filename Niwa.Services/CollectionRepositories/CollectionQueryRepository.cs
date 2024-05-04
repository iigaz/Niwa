using Microsoft.EntityFrameworkCore;
using Niwa.Database;
using Niwa.Models;

namespace Niwa.Services.CollectionRepositories;

public class CollectionQueryRepository(ApplicationDbContext context) : ICollectionQueryRepository
{
    public Task<Collection?> GetNoteCollection(Guid userId, Note note)
    {
        return context.Collections.SingleOrDefaultAsync(collection =>
            collection.UserId == userId && collection.Notes.Contains(note));
    }
}