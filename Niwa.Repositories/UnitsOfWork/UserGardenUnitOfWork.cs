using Microsoft.EntityFrameworkCore.Storage;
using Niwa.Database;
using Niwa.Repositories.GardenRepositories.Write;
using Niwa.Repositories.UserRepositories.Write;

namespace Niwa.Repositories.UnitsOfWork;

public class UserGardenUnitOfWork(ApplicationDbContext context) : IUserGardenUnitOfWork
{
    public IUserWriteRepository UserWriteRepository => new UserWriteRepository(context);
    public IGardenWriteRepository GardenWriteRepository => new GardenWriteRepository(context);

    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return context.Database.BeginTransactionAsync();
    }
}