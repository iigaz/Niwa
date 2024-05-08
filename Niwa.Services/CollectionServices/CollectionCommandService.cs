using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using Niwa.Models;
using Niwa.Services.CollectionRepositories;

namespace Niwa.Services.CollectionServices;

public class CollectionCommandService(
    ILogger<CollectionCommandService> logger,
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
        {
            logger.LogWarning("Tried to create a collection but could not validate it.");
            return null;
        }

        await collectionCommandRepository.CreateCollectionAsync(collection);
        return id;
    }

    public async Task<bool> ChangeCollectionAsync(Guid userId, Note note, Guid? newCollectionId)
    {
        var collection = await collectionQueryRepository.GetNoteCollectionAsync(userId, note);
        var newCollection = newCollectionId != null
            ? await collectionQueryRepository.GetByIdAsync(newCollectionId.Value)
            : null;
        if ((newCollectionId != null && newCollection == null) ||
            (newCollection != null && newCollection.UserId != userId))
        {
            logger.LogWarning("Tried to change collection but either didn't found new one or user didn't own it.");
            return false;
        }

        await collectionCommandRepository.ChangeCollectionAsync(collection, note, newCollection);
        return true;
    }
}