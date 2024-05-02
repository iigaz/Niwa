using Microsoft.EntityFrameworkCore.Storage;
using Niwa.Repositories.GardenRepositories.Write;
using Niwa.Repositories.UserRepositories.Write;

namespace Niwa.Repositories.UnitsOfWork;

public interface IUserGardenUnitOfWork
{
    public IUserWriteRepository UserWriteRepository { get; }
    public IGardenWriteRepository GardenWriteRepository { get; }

    public Task<IDbContextTransaction> BeginTransactionAsync();
}