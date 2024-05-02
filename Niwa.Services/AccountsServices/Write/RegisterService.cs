using System.ComponentModel.DataAnnotations;
using Niwa.Models;
using Niwa.Repositories.RoleRepositories.Read;
using Niwa.Repositories.UnitsOfWork;
using Niwa.Repositories.UserRepositories.Read;

namespace Niwa.Services.AccountsServices.Write;

public class RegisterService(
    IRoleReadRepository roleReadRepository,
    IUserReadRepository userReadRepository,
    IUserGardenUnitOfWork unitOfWork)
    : IRegisterService
{
    /// <summary>
    ///     Create a user and a garden.
    /// </summary>
    public async Task<List<ValidationResult>?> RegisterAsync(string username, string password)
    {
        var validated = true;
        var validationResults = new List<ValidationResult>();

        var existingUser = await userReadRepository.GetUserByUsernameAsync(username);
        if (existingUser != null)
        {
            validated = false;
            validationResults.Add(new ValidationResult("User with this username already exists."));
        }

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

        validated &= Validator.TryValidateObject(user, new ValidationContext(user), validationResults);
        validated &= Validator.TryValidateObject(garden, new ValidationContext(garden), validationResults);

        await using var transaction = await unitOfWork.BeginTransactionAsync();
        await unitOfWork.UserWriteRepository.CreateAsync(user);
        await unitOfWork.GardenWriteRepository.CreateAsync(garden);
        await transaction.CommitAsync();

        return !validated ? validationResults : null;
    }
}