using System.ComponentModel.DataAnnotations;
using Niwa.Models;
using Niwa.Services.CollectionRepositories;

namespace Niwa.Services.CollectionServices;

public class CollectionCommandService(
    ICollectionCommandRepository collectionCommandRepository,
    ICollectionQueryRepository collectionQueryRepository)
    : ICollectionCommandService
{
    public async Task<Guid?> CreateCollectionAsync(Guid userId, string title)
    {
        var id = Guid.NewGuid();
        var collection = new Collection
        {
            Id = id,
            Title = title,
            UserId = userId
        };
        if (!Validator.TryValidateObject(collection, new ValidationContext(collection), new List<ValidationResult>()))
            return null;
        await collectionCommandRepository.CreateCollectionAsync(collection);
        return id;
    }

    public async Task<bool> ChangeCollectionAsync(Guid userId, Note note, Guid? newCollectionId)
    {
        var collection = await collectionQueryRepository.GetNoteCollection(userId, note);
        var newCollection = newCollectionId != null
            ? await collectionQueryRepository.GetById(newCollectionId.Value)
            : null;
        if ((newCollectionId != null && newCollection == null) ||
            (newCollection != null && newCollection.UserId != userId))
            return false;
        await collectionCommandRepository.ChangeCollectionAsync(collection, note, newCollection);
        return true;
    }
}