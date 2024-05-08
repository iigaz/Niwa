namespace Niwa.Options;

public class TurnstileOptions
{
    public const string Section = "Turnstile";

    public string SiteKey { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
    public string VerificationUrl { get; set; } = null!;
}