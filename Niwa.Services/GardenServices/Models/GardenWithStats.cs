using Niwa.Models;

namespace Niwa.Services.GardenServices.Models;

public class GardenWithStats : Garden
{
    public int PublicNoteCount { get; set; }

    public ICollection<string> PopularTags { get; set; } = new List<string>();

    public static GardenWithStats From(Garden garden)
    {
        return new GardenWithStats
        {
            Id = garden.Id,
            Title = garden.Title,
            UserId = garden.UserId,
            User = garden.User,
            Summary = garden.Summary,
            Subscribers = garden.Subscribers,
            Notes = garden.Notes,
            FeaturedNotes = garden.FeaturedNotes,
            CreatedDateTime = garden.CreatedDateTime,
            UpdatedDateTime = garden.UpdatedDateTime
        };
    }
}