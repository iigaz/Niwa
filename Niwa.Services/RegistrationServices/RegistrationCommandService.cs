using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Niwa.Models;
using Niwa.Services.UnitsOfWork;
using Niwa.Services.UserRepositories;

namespace Niwa.Services.RegistrationServices;

public class RegistrationCommandService(
    IUserQueryRepository userQueryRepository,
    IUserGardenUnitOfWork unitOfWork)
    : IRegistrationCommandService
{
    /// <summary>
    ///     Create a user and a garden.
    /// </summary>
    public async Task<List<ValidationResult>?> RegisterAsync(string username, string password)
    {
        var validated = true;
        var validationResults = new List<ValidationResult>();
        var existingUser = await userQueryRepository.GetUsers().SingleOrDefaultAsync(user => user.Username == username);
        if (existingUser != null)
        {
            validated = false;
            validationResults.Add(new ValidationResult("User with this username already exists."));
        }

        var userId = Guid.NewGuid();
        var title = $"{username}'s Garden";

        var user = new User
        {
            Id = userId,
            Username = username,
            EmailAddress = null,
            PasswordHash = User.HashPassword(password)
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
        await unitOfWork.UserCommandRepository.CreateAsync(user);
        await unitOfWork.GardenCommandRepository.CreateAsync(garden);
        await transaction.CommitAsync();

        return !validated ? validationResults : null;
    }
}