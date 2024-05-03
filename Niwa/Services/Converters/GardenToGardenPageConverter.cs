using Niwa.Dtos.GardenDtos.Read;
using Niwa.Models;
using Niwa.Models.Enums;

namespace Niwa.Services.Converters;

public class GardenToGardenPageConverter(INoteToNoteCardConverter noteCardConverter) : IGardenToGardenPageConverter
{
    public GardenPageDto Convert(Garden garden, bool onlyPublicNotes = true)
    {
        return new GardenPageDto
        {
            Title = garden.Title,
            Summary = garden.Summary,
            FeaturedNotes = (onlyPublicNotes
                ? garden.FeaturedNotes.Where(note => note.Access == Access.Public)
                : garden.FeaturedNotes).Select(noteCardConverter.Convert).ToList()
        };
    }
}