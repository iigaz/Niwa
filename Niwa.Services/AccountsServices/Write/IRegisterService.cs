using System.ComponentModel.DataAnnotations;

namespace Niwa.Services.AccountsServices.Write;

public interface IRegisterService
{
    public Task<List<ValidationResult>?> RegisterAsync(string username, string password);
}