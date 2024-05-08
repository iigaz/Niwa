using Minio;
using Niwa.Options;

namespace Niwa.Extensions.ServiceCollectionExtensions;

public static class AddMinioConfiguredExtensions
{
    public static IServiceCollection AddMinioConfigured(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var options = new MinIoOptions();
        configuration.GetSection(MinIoOptions.Section).Bind(options);
        serviceCollection.AddMinio(configureClient => configureClient
            .WithEndpoint(options.Endpoint)
            .WithCredentials(options.AccessKey, options.SecretKey)
            .WithSSL(false));
        return serviceCollection;
    }
}