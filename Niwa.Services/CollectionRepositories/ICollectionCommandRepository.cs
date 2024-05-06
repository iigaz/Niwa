using Niwa.Models;

namespace Niwa.Services.CollectionRepositories;

public interface ICollectionCommandRepository
{
    public Task ChangeCollectionAsync(Collection? oldCollection, Note note, Collection? newCollection);
    public Task CreateCollectionAsync(Collection collection);
}