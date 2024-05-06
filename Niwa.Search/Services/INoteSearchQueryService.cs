using Niwa.Search.Models;

namespace Niwa.Search.Services;

public interface INoteSearchQueryService
{
    public Task<List<NoteSearchModel>> SearchNotesAsync(string query, Guid currentUserId);

    public Task<List<NoteSearchModel>> SearchGardenNotesAsync(string query, string authorUsername,
        Guid currentUserId);

    public Task<List<NoteSearchModel>> SearchTagNotesAsync(string query, string tag, Guid currentUserId);
    public Task<List<NoteSearchModel>> SearchCollectionNotesAsync(string query, IEnumerable<Guid> noteIds);
}