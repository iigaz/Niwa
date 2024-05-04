using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Niwa.Components;
using Niwa.Database;
using Niwa.Services;
using Niwa.Services.CollectionRepositories;
using Niwa.Services.CollectionServices;
using Niwa.Services.Converters;
using Niwa.Services.GardenRepositories;
using Niwa.Services.GardenServices;
using Niwa.Services.LoginServices;
using Niwa.Services.NoteRepositories;
using Niwa.Services.NoteServices;
using Niwa.Services.RegistrationServices;
using Niwa.Services.RoleRepositories;
using Niwa.Services.UnitsOfWork;
using Niwa.Services.UserRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddAuthorization();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddScoped<IGardenCommandRepository, GardenCommandRepository>();
builder.Services.AddScoped<IGardenQueryRepository, GardenQueryRepository>();
builder.Services.AddScoped<IGardenQueryService, GardenQueryService>();
builder.Services.AddScoped<ILoginQueryService, LoginQueryService>();
builder.Services.AddScoped<IRegistrationCommandService, RegistrationCommandService>();
builder.Services.AddScoped<IRoleQueryRepository, RoleQueryRepository>();
builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();
builder.Services.AddScoped<IUserQueryRepository, UserQueryRepository>();
builder.Services.AddScoped<IUserGardenUnitOfWork, UserGardenUnitOfWork>();
builder.Services.AddScoped<ILinkManager, LinkManager>();
builder.Services.AddScoped<IGardenToGardenPageConverter, GardenToGardenPageConverter>();
builder.Services.AddScoped<INoteToNoteCardConverter, NoteToNoteCardConverter>();
builder.Services.AddScoped<INoteToNotePageConverter, NoteToNotePageConverter>();
builder.Services.AddScoped<IGardenToGardenLinkInfoConverter, GardenToGardenLinkInfoConverter>();
builder.Services.AddScoped<INoteFileToNoteFileConverter, NoteFileToNoteFileConverter>();
builder.Services.AddScoped<INoteQueryRepository, NoteQueryRepository>();
builder.Services.AddScoped<INoteQueryService, NoteQueryService>();
builder.Services.AddScoped<ICollectionQueryRepository, CollectionQueryRepository>();
builder.Services.AddScoped<ICollectionQueryService, CollectionQueryService>();
builder.Services.AddScoped<ICollectionToCollectionConverter, CollectionToCollectionConverter>();
builder.Services.AddScoped<IGardenCommandService, GardenCommandService>();


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

app.Run();