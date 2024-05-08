namespace Niwa.Options;

public class OpenSearchOptions
{
    public const string Section = "OpenSearch";

    public string BaseUrl { get; set; } = null!;
    public string DefaultIndex { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}