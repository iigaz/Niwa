using System.Security.Claims;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Niwa.Components;
using Niwa.Database;
using Niwa.Extensions.ServiceCollectionExtensions;
using Niwa.Options;
using Niwa.Services.BackgroundJobServices;
using Niwa.Services.NewsServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddAuthorization();

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddHangfire(configuration => configuration.UsePostgreSqlStorage(options =>
    options.UseNpgsqlConnection(builder.Configuration.GetConnectionString("HangfireDatabase"))));
builder.Services.AddHangfireServer();

builder.Services.AddOpenSearch(builder.Configuration);
builder.Services.AddMinioConfigured(builder.Configuration);

builder.Services.AddRepositories();
builder.Services.AddModelsServices();
builder.Services.AddHelperServices();
builder.Services.AddSearchServices();
builder.Services.AddConverters();

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
else
{
    app.UseHangfireDashboard(); //Will be available under http://localhost:5000/hangfire"
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Niwa API V0"); });
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapGet("/feed/news.json", async (ClaimsPrincipal principal, [FromServices] INewsQueryService newsQueryService) =>
{
    var currentUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
    var parsed = Guid.TryParse(currentUserId, out var userId);
    return !parsed ? Results.Unauthorized() : Results.Ok(await newsQueryService.GetNewsAsync(userId, 250));
});

BackgroundJob.Enqueue<BackgroundNoteIndexingService>(x => x.IndexAllNotesAsync());

app.Run();