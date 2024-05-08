using Niwa.Models;

namespace Niwa.Services.CollectionRepositories;

public interface ICollectionQueryRepository
{
    public Task<Collection?> GetNoteCollectionAsync(Guid userId, Note note);
    public Task<Collection?> GetByIdAsync(Guid collectionId);
    public Task<List<Collection>> GetUserCollectionsAsync(Guid userId);
}