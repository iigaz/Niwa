using Microsoft.EntityFrameworkCore.Storage;
using Niwa.Services.GardenRepositories;
using Niwa.Services.UserRepositories;

namespace Niwa.Services.UnitsOfWork;

public interface IUserGardenUnitOfWork
{
    public IUserCommandRepository UserCommandRepository { get; }
    public IGardenCommandRepository GardenCommandRepository { get; }
    public Task<IDbContextTransaction> BeginTransactionAsync();
}