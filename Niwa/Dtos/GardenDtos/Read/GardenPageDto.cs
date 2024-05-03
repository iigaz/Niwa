using Niwa.Dtos.NoteDtos.Read;

namespace Niwa.Dtos.GardenDtos.Read;

public class GardenPageDto
{
    public string Title { get; set; } = null!;

    public string Summary { get; set; } = null!;

    public List<NoteCardDto> FeaturedNotes { get; set; } = null!;
}