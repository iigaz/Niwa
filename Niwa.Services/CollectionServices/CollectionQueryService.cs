using Niwa.Models;
using Niwa.Services.CollectionRepositories;

namespace Niwa.Services.CollectionServices;

public class CollectionQueryService(ICollectionQueryRepository collectionQueryRepository) : ICollectionQueryService
{
    public Task<Collection?> GetNoteCollection(Guid userId, Note note)
    {
        return collectionQueryRepository.GetNoteCollection(userId, note);
    }

    public Task<Collection?> GetById(Guid collectionId)
    {
        return collectionQueryRepository.GetById(collectionId);
    }

    public Task<List<Collection>> GetUserCollections(Guid userId)
    {
        return collectionQueryRepository.GetUserCollections(userId);
    }
}