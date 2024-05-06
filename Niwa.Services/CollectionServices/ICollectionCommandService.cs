using Niwa.Models;

namespace Niwa.Services.CollectionServices;

public interface ICollectionCommandService
{
    public Task<bool> ChangeCollectionAsync(Guid userId, Note note, Guid? newCollectionId);
    public Task<Guid?> CreateCollectionAsync(Guid userId, string title);
}