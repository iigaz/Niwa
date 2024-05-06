using Niwa.Models;
using Niwa.Search.Models;
using OpenSearch.Client;

namespace Niwa.Search.Services;

public class NoteSearchCommandService(IOpenSearchClient openSearchClient) : INoteSearchCommandService
{
    public async Task AddNoteToIndexAsync(Note note)
    {
        var noteSearchModel = NoteSearchModel.From(note);

        await openSearchClient.IndexDocumentAsync(noteSearchModel);
    }

    public async Task UpdateNoteAsync(Note note)
    {
        var noteSearchModel = NoteSearchModel.From(note);

        await openSearchClient.UpdateAsync<NoteSearchModel>(noteSearchModel,
            i => i.Index<NoteSearchModel>().Doc(noteSearchModel));
    }
}