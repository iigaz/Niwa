using Niwa.Extensions;
using Niwa.Services.GardenServices.Models;

namespace Niwa.Dtos.GardenDtos.Read;

public class GardenCardDto
{
    public string? Title { get; set; }

    public string UrlId { get; set; }

    public string? Description { get; set; }

    public int ActiveSinceYear { get; set; }

    public string? LastActivityRelative { get; set; }

    public int PublicNotesCount { get; set; }

    public ICollection<string> Tags { get; set; } = new List<string>();

    public static GardenCardDto From(GardenWithStats garden)
    {
        return new GardenCardDto
        {
            Title = garden.Title,
            UrlId = garden.User.Username,
            Description = garden.Summary,
            ActiveSinceYear = garden.CreatedDateTime.Year,
            LastActivityRelative = garden.UpdatedDateTime.TimeAgo(),
            PublicNotesCount = garden.PublicNoteCount,
            Tags = garden.PopularTags
        };
    }
}