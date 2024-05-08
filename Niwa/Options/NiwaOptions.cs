namespace Niwa.Options;

public class NiwaOptions
{
    public const string Section = "Niwa";

    public IEnumerable<Guid> FeaturedGardens { get; set; } = null!;
    public int NewsOnMainPage { get; set; }
    public bool RegistrationOpen { get; set; }
    public int MaxUploadSizeBytes { get; set; }
}