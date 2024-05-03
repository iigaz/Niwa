using Niwa.Dtos.NoteDtos.Read;
using Niwa.Models;
using Niwa.Models.Enums;

namespace Niwa.Dtos.GardenDtos.Read;

public class GardenPageDto
{
    public string Title { get; set; } = null!;

    public string Summary { get; set; } = null!;

    public List<NoteCardDto> FeaturedNotes { get; set; } = null!;

    public static GardenPageDto From(Garden garden, bool onlyPublicNotes = true)
    {
        return new GardenPageDto
        {
            Title = garden.Title,
            Summary = garden.Summary,
            FeaturedNotes = (onlyPublicNotes
                ? garden.FeaturedNotes.Where(note => note.Access == Access.Public)
                : garden.FeaturedNotes).Select(NoteCardDto.From).ToList()
        };
    }
}