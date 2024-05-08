using Niwa.Services.CollectionRepositories;
using Niwa.Services.CommentRepositories;
using Niwa.Services.GardenRepositories;
using Niwa.Services.NoteRepositories;
using Niwa.Services.UserRepositories;

namespace Niwa.Extensions.ServiceCollectionExtensions;

public static class AddRepositoriesExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IGardenCommandRepository, GardenCommandRepository>();
        serviceCollection.AddScoped<IGardenQueryRepository, GardenQueryRepository>();
        serviceCollection.AddScoped<IUserCommandRepository, UserCommandRepository>();
        serviceCollection.AddScoped<IUserQueryRepository, UserQueryRepository>();
        serviceCollection.AddScoped<INoteQueryRepository, NoteQueryRepository>();
        serviceCollection.AddScoped<ICollectionQueryRepository, CollectionQueryRepository>();
        serviceCollection.AddScoped<INoteCommandRepository, NoteCommandRepository>();
        serviceCollection.AddScoped<ICollectionQueryRepository, CollectionQueryRepository>();
        serviceCollection.AddScoped<ICollectionCommandRepository, CollectionCommandRepository>();
        serviceCollection.AddScoped<ICommentQueryRepository, CommentQueryRepository>();
        serviceCollection.AddScoped<ICommentCommandRepository, CommentCommandRepository>();
        return serviceCollection;
    }
}