using Niwa.Models.Enums;
using Niwa.Search.Models;
using OpenSearch.Client;

namespace Niwa.Search.Services;

public class NoteSearchQueryService(IOpenSearchClient client) : INoteSearchQueryService
{
    public async Task<List<NoteSearchModel>> SearchNotesAsync(string query, Guid currentUserId)
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
                                .Field(model => model.Tags))))
                    .Filter(fq => fq
                        .Bool(fbq => fbq
                            .Should(sq => sq
                                    .Term(t => t
                                        .Field(model => model.Access).Value(Access.Public)),
                                sq => sq
                                    .Term(t => t
                                        .Field(model => model.AuthorId).Value(currentUserId))))))));
        return !response.IsValid ? [] : response.Documents.ToList();
    }

    public async Task<List<NoteSearchModel>> SearchGardenNotesAsync(string query, string authorUsername,
        Guid currentUserId)
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
                                .Field(model => model.Tags))))
                    .Filter(fq => fq
                        .Bool(fbq => fbq
                            .Must(mq => mq
                                    .Bool(bbq => bbq
                                        .Should(sq => sq
                                                .Term(t => t
                                                    .Field(model => model.Access).Value(Access.Public)),
                                            sq => sq
                                                .Term(t => t
                                                    .Field(model => model.AuthorId).Value(currentUserId)))),
                                mq => mq
                                    .Term(t => t
                                        .Field(model => model.Author).Value(authorUsername))))))));
        return !response.IsValid ? [] : response.Documents.ToList();
    }

    public async Task<List<NoteSearchModel>> SearchTagNotesAsync(string query, string tag, Guid currentUserId)
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
                                .Field(model => model.Author))))
                    .Filter(fq => fq
                        .Bool(fbq => fbq
                            .Must(mq => mq
                                    .Bool(bbq => bbq
                                        .Should(sq => sq
                                                .Term(t => t
                                                    .Field(model => model.Access).Value(Access.Public)),
                                            sq => sq
                                                .Term(t => t
                                                    .Field(model => model.AuthorId).Value(currentUserId)))),
                                mq => mq
                                    .Term(t => t
                                        .Field(model => model.Tags).Value(tag))))))));
        return !response.IsValid ? [] : response.Documents.ToList();
    }

    public async Task<List<NoteSearchModel>> SearchCollectionNotesAsync(string query, IEnumerable<Guid> noteIds)
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
                                .Field(model => model.Tags))))
                    .Filter(fq => fq.Ids(iq => iq.Values(noteIds))))));
        return !response.IsValid ? [] : response.Documents.ToList();
    }
}