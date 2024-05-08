using System.Text.Json;
using Niwa.Dtos.TurnstileDtos;

namespace Niwa.Services;

public class TurnstileValidatorService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    : ITurnstileValidatorService
{
    public async Task<bool> ValidateAsync(string token)
    {
        var httpClient = httpClientFactory.CreateClient();
        var secret = configuration["Turnstile:SecretKey"];
        var url = configuration["Turnstile:VerificationUrl"];
        var data = new FormUrlEncodedContent(new Dictionary<string, string?>
        {
            { "secret", secret }, { "response", token }
        });
        var response = await httpClient.PostAsync(url, data);
        var responseDto = await JsonSerializer.DeserializeAsync<TurnstileVerificationResponseDto>(
            await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        return responseDto?.Success ?? false;
    }
}