using Niwa.Search.Services;
using Niwa.Services.NoteRepositories;

namespace Niwa.Services.BackgroundJobServices;

public class BackgroundNoteIndexingService(
    INoteQueryRepository noteQueryRepository,
    INoteSearchCommandService noteSearchCommandService)
{
    public async Task IndexAllNotesAsync()
    {
        var notes = await noteQueryRepository.GetAllNotesForIndexingAsync();
        foreach (var note in notes) await noteSearchCommandService.AddNoteToIndexAsync(note);
    }
}