using System.ComponentModel.DataAnnotations;
using Niwa.Models;
using Niwa.Repositories.RoleRepositories.Read;
using Niwa.Repositories.UnitsOfWork;

namespace Niwa.Services.AccountsServices.Write;

public class RegisterService(IRoleReadRepository roleReadRepository, IUserGardenUnitOfWork unitOfWork)
    : IRegisterService
{
    /// <summary>
    ///     Create a user and a garden. Assumes that username and password were validated before this method was called (e.g.
    ///     on form submit).
    ///     <exception cref="ValidationException">Could not validate either username, password or garden name.</exception>
    /// </summary>
    public async Task RegisterAsync(string username, string password)
    {
        var roles = await roleReadRepository.GetRolesAsync();
        var userId = Guid.NewGuid();
        var title = $"{username}'s Garden";

        var user = new User
        {
            Id = userId,
            Username = username,
            EmailAddress = null,
            PasswordHash = User.HashPassword(password),
            Roles = roles[..3] // Every user has the first three roles.
        };
        var garden = new Garden
        {
            Id = Guid.NewGuid(),
            Title = title,
            UserId = userId,
            Summary = $"Welcome to {title}."
        };

        Validator.ValidateObject(user, new ValidationContext(user));
        Validator.ValidateObject(garden, new ValidationContext(garden));

        await using var transaction = await unitOfWork.BeginTransactionAsync();
        await unitOfWork.GardenWriteRepository.CreateAsync(garden);
        await unitOfWork.UserWriteRepository.CreateAsync(user);
        await transaction.CommitAsync();
    }
}