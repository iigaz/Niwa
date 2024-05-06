using Niwa.Search.Models;

namespace Niwa.Search.Services;

public interface INoteSearchQueryService
{
    public Task<List<NoteSearchModel>> SearchNotesAsync(string query);
    public Task<List<NoteSearchModel>> SearchGardenNotesAsync(string query);
    public Task<List<NoteSearchModel>> SearchCollectionNotesAsync(string query);
    public Task<List<NoteSearchModel>> SearchTagNotesAsync(string query);
}