using Niwa.Options;
using Niwa.Search.Models;
using OpenSearch.Client;

namespace Niwa.Extensions.ServiceCollectionExtensions;

public static class AddSearchExtensions
{
    public static void AddOpenSearch(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new OpenSearchOptions();
        configuration.GetSection(OpenSearchOptions.Section).Bind(options);
        var settings = new ConnectionSettings(new Uri(options.BaseUrl))
            .BasicAuthentication(options.Username, options.Password).DefaultIndex(options.DefaultIndex);
        AddDefaultMappings(settings);
        var client = new OpenSearchClient(settings);
        CreateIndex(client, options.DefaultIndex);
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