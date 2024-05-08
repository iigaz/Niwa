using Niwa.Search.Services;
using Niwa.Services;
using Niwa.Services.BackgroundJobServices;
using Niwa.Services.CollectionServices;
using Niwa.Services.CommentServices;
using Niwa.Services.Converters;
using Niwa.Services.GardenServices;
using Niwa.Services.LoginServices;
using Niwa.Services.NewsServices;
using Niwa.Services.NoteServices;
using Niwa.Services.RegistrationServices;
using Niwa.Services.SubscriptionServices;
using Niwa.Services.UnitsOfWork;

namespace Niwa.Extensions.ServiceCollectionExtensions;

public static class AddServicesExtensions
{
    public static IServiceCollection AddModelsServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IGardenQueryService, GardenQueryService>();
        serviceCollection.AddScoped<ILoginQueryService, LoginQueryService>();
        serviceCollection.AddScoped<IRegistrationCommandService, RegistrationCommandService>();
        serviceCollection.AddScoped<IUserGardenUnitOfWork, UserGardenUnitOfWork>();
        serviceCollection.AddScoped<INoteQueryService, NoteQueryService>();
        serviceCollection.AddScoped<ICollectionQueryService, CollectionQueryService>();
        serviceCollection.AddScoped<IGardenCommandService, GardenCommandService>();
        serviceCollection.AddScoped<INoteCommandService, NoteCommandService>();
        serviceCollection.AddScoped<ICollectionQueryService, CollectionQueryService>();
        serviceCollection.AddScoped<ICollectionCommandService, CollectionCommandService>();
        serviceCollection.AddScoped<ICommentQueryService, CommentQueryService>();
        serviceCollection.AddScoped<ICommentCommandService, CommentCommandService>();
        serviceCollection.AddScoped<ISubscriptionCommandService, SubscriptionCommandService>();
        serviceCollection.AddScoped<INewsQueryService, NewsQueryService>();
        return serviceCollection;
    }

    public static IServiceCollection AddConverters(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IGardenToGardenPageConverter, GardenToGardenPageConverter>();
        serviceCollection.AddScoped<INoteToNoteCardConverter, NoteToNoteCardConverter>();
        serviceCollection.AddScoped<INoteToNotePageConverter, NoteToNotePageConverter>();
        serviceCollection.AddScoped<IGardenToGardenLinkInfoConverter, GardenToGardenLinkInfoConverter>();
        serviceCollection.AddScoped<INoteFileToNoteFileConverter, NoteFileToNoteFileConverter>();
        serviceCollection.AddScoped<ICollectionToCollectionConverter, CollectionToCollectionConverter>();
        return serviceCollection;
    }

    public static IServiceCollection AddHelperServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ILinkManager, LinkManager>();
        serviceCollection.AddScoped<IFileUploadService, FileUploadService>();
        serviceCollection.AddScoped<IFileDownloadService, FileDownloadService>();
        serviceCollection.AddScoped<ITurnstileValidatorService, TurnstileValidatorService>();
        return serviceCollection;
    }

    public static IServiceCollection AddSearchServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<INoteSearchCommandService, NoteSearchCommandService>();
        serviceCollection.AddScoped<INoteSearchQueryService, NoteSearchQueryService>();
        serviceCollection.AddScoped<BackgroundNoteIndexingService>();
        return serviceCollection;
    }
}