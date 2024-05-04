using Niwa.Extensions;
using Niwa.Services.GardenServices.Models;

namespace Niwa.Dtos.GardenDtos;

public class GardenCardQueryDto
{
    public string? Title { get; set; }

    public string AuthorUsername { get; set; } = null!;

    public string? Description { get; set; }

    public int ActiveSinceYear { get; set; }

    public string? LastActivityRelative { get; set; }

    public int PublicNotesCount { get; set; }

    public ICollection<string> Tags { get; set; } = new List<string>();

    public static GardenCardQueryDto From(GardenWithStats garden)
    {
        return new GardenCardQueryDto
        {
            Title = garden.Title,
            AuthorUsername = garden.User.Username,
            Description = garden.Summary,
            ActiveSinceYear = garden.CreatedDateTime.Year,
            LastActivityRelative = garden.UpdatedDateTime.TimeAgo(),
            PublicNotesCount = garden.PublicNoteCount,
            Tags = garden.PopularTags
        };
    }
}