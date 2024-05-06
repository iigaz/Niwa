using Niwa.Search.Models;
using OpenSearch.Client;

namespace Niwa.Extensions;

public static class SearchExtensions
{
    public static void AddOpenSearch(this IServiceCollection services, IConfiguration configuration)
    {
        var baseUrl = configuration["OpenSearch:BaseUrl"]!;
        var index = configuration["OpenSearch:DefaultIndex"]!;
        var username = configuration["OpenSearch:Username"]!;
        var password = configuration["OpenSearch:Password"]!;
        var settings = new ConnectionSettings(new Uri(baseUrl))
            .BasicAuthentication(username, password).DefaultIndex(index);
        AddDefaultMappings(settings);
        var client = new OpenSearchClient(settings);
        CreateIndex(client, index);
        services.AddSingleton<IOpenSearchClient>(client);
    }

    private static void AddDefaultMappings(ConnectionSettings settings)
    {
        settings.DefaultMappingFor<NoteSearchModel>(descriptor => descriptor);
    }

    private static void CreateIndex(IOpenSearchClient client, string indexName)
    {
        var createIndexResponse =
            client.Indices.Create(indexName, index => index.Map<NoteSearchModel>(x => x.AutoMap()));
    }
}