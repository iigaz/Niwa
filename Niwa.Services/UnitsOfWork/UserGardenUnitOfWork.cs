using Microsoft.EntityFrameworkCore.Storage;
using Niwa.Database;
using Niwa.Services.GardenRepositories;
using Niwa.Services.UserRepositories;

namespace Niwa.Services.UnitsOfWork;

public class UserGardenUnitOfWork(ApplicationDbContext context) : IUserGardenUnitOfWork
{
    public IUserCommandRepository UserCommandRepository => new UserCommandRepository(context);
    public IGardenCommandRepository GardenCommandRepository => new GardenCommandRepository(context);

    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return context.Database.BeginTransactionAsync();
    }
}