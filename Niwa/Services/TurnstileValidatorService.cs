using System.Text.Json;
using Microsoft.Extensions.Options;
using Niwa.Dtos.TurnstileDtos;
using Niwa.Options;

namespace Niwa.Services;

public class TurnstileValidatorService(
    IHttpClientFactory httpClientFactory,
    IOptionsMonitor<TurnstileOptions> optionsMonitor)
    : ITurnstileValidatorService
{
    public async Task<bool> ValidateAsync(string token)
    {
        var httpClient = httpClientFactory.CreateClient();
        var secret = optionsMonitor.CurrentValue.SecretKey;
        var url = optionsMonitor.CurrentValue.VerificationUrl;
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