using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Niwa.Components;
using Niwa.Database;
using Niwa.Extensions.ServiceCollectionExtensions;
using Niwa.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddAuthorization();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddOpenSearch(builder.Configuration);
builder.Services.AddMinioConfigured(builder.Configuration);

builder.Services.AddHttpClient();

builder.Services.AddRepositories();
builder.Services.AddModelsServices();
builder.Services.AddHelperServices();
builder.Services.AddSearchServices();

builder.Services.Configure<NiwaOptions>(builder.Configuration.GetSection(NiwaOptions.Section));
builder.Services.Configure<MinIoOptions>(builder.Configuration.GetSection(MinIoOptions.Section));
builder.Services.Configure<TurnstileOptions>(builder.Configuration.GetSection(TurnstileOptions.Section));
builder.Services.Configure<OpenSearchOptions>(builder.Configuration.GetSection(OpenSearchOptions.Section));
builder.Services.Configure<SqidsOptions>(builder.Configuration.GetSection(SqidsOptions.Section));

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// This endpoint is used to add all notes into index
// app.MapGet("/test-do-not-click",
//     async (INoteQueryRepository noteQueryRepository, INoteSearchCommandService noteSearchCommandService) =>
//     {
//         var notes = await noteQueryRepository.GetNotes().Include(note => note.Garden).Include(note => note.Tags)
//             .Include(note => note.User)
//             .Include(note => note.LatestRevision).ToListAsync();
//         foreach (var note in notes) await noteSearchCommandService.AddNoteToIndexAsync(note);
//     });

app.Run();