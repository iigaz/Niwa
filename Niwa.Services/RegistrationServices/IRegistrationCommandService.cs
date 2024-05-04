using System.ComponentModel.DataAnnotations;

namespace Niwa.Services.RegistrationServices;

public interface IRegistrationCommandService
{
    public Task<List<ValidationResult>?> RegisterAsync(string username, string password);
}