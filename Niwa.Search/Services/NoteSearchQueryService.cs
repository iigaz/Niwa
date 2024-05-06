using Niwa.Search.Models;
using OpenSearch.Client;

namespace Niwa.Search.Services;

public class NoteSearchQueryService(IOpenSearchClient client) : INoteSearchQueryService
{
    public async Task<List<NoteSearchModel>> SearchNotesAsync(string query)
    {
        var response = await client.SearchAsync<NoteSearchModel>(s => s
            .Index<NoteSearchModel>()
            .Query(q => q
                .Bool(bq => bq
                    .Must(mq => mq
                        .SimpleQueryString(sqs => sqs
                            .Query(query)
                            .DefaultOperator(Operator.And)
                            .Fields(f => f
                                .Field(model => model.Title)
                                .Field(model => model.Summary)
                                .Field(model => model.Content)
                                .Field(model => model.Author)
                                .Field(model => model.Tags)))))));
        return !response.IsValid ? [] : response.Documents.ToList();
    }

    public async Task<List<NoteSearchModel>> SearchGardenNotesAsync(string query)
    {
        throw new NotImplementedException();
    }

    public async Task<List<NoteSearchModel>> SearchCollectionNotesAsync(string query)
    {
        throw new NotImplementedException();
    }

    public async Task<List<NoteSearchModel>> SearchTagNotesAsync(string query)
    {
        throw new NotImplementedException();
    }
}