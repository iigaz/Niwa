using Niwa.Models;

namespace Niwa.Services.CollectionRepositories;

public interface ICollectionQueryRepository
{
    public Task<Collection?> GetNoteCollection(Guid userId, Note note);
    public Task<Collection?> GetById(Guid collectionId);
    public Task<List<Collection>> GetUserCollections(Guid userId);
}