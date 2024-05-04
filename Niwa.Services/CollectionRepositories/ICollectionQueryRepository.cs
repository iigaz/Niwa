using Niwa.Models;

namespace Niwa.Services.CollectionRepositories;

public interface ICollectionQueryRepository
{
    public Task<Collection?> GetNoteCollection(Guid userId, Note note);
}