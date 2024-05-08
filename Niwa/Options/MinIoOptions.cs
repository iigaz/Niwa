namespace Niwa.Options;

public class MinIoOptions
{
    public const string Section = "MinIO";

    public string Endpoint { get; set; } = null!;
    public string AccessKey { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
    public string Bucket { get; set; } = null!;
}