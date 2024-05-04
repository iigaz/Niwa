using Mapster;
using Niwa.Models;

namespace Niwa.Services.GardenServices.Models;

public class GardenWithStats : Garden
{
    public int PublicNoteCount { get; set; }

    public ICollection<string> PopularTags { get; set; } = new List<string>();

    public static GardenWithStats From(Garden garden)
    {
        return garden.Adapt<GardenWithStats>();
    }
}