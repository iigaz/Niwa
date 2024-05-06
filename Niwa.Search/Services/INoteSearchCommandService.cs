using Niwa.Models;

namespace Niwa.Search.Services;

public interface INoteSearchCommandService
{
    public Task AddNoteToIndexAsync(Note note);
    public Task UpdateNoteAsync(Note note);
}