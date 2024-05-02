namespace Niwa.Services.AccountsServices.Write;

public interface IRegisterService
{
    public Task RegisterAsync(string username, string password);
}