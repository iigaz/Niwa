using Niwa.Models;

namespace Niwa.Services.CollectionServices;

public interface ICollectionQueryService
{
    public Task<Collection?> GetNoteCollection(Guid userId, Note note);
}