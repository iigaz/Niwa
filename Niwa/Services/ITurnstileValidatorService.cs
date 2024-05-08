namespace Niwa.Services;

public interface ITurnstileValidatorService
{
    public Task<bool> ValidateAsync(string token);
}