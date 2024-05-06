using Niwa.Database;
using Niwa.Models;

namespace Niwa.Services.CollectionRepositories;

public class CollectionCommandRepository(
    ApplicationDbContext context) : ICollectionCommandRepository
{
    public async Task ChangeCollectionAsync(Collection? oldCollection, Note note, Collection? newCollection)
    {
        if (oldCollection != null)
        {
            oldCollection.Notes.Remove(note);
            oldCollection.UpdatedDateTime = DateTime.UtcNow;
            context.Collections.Update(oldCollection);
        }

        if (newCollection != null)
        {
            newCollection.Notes.Add(note);
            newCollection.UpdatedDateTime = DateTime.UtcNow;
            context.Collections.Update(newCollection);
        }

        await context.SaveChangesAsync();
    }

    public async Task CreateCollectionAsync(Collection collection)
    {
        collection.CreatedDateTime = DateTime.UtcNow;
        collection.UpdatedDateTime = DateTime.UtcNow;
        await context.Collections.AddAsync(collection);
        await context.SaveChangesAsync();
    }
}