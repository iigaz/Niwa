using Niwa.Dtos.NoteDtos;

namespace Niwa.Dtos.GardenDtos;

public class GardenPageQueryDto
{
    public string Title { get; set; } = null!;

    public string Summary { get; set; } = null!;

    public List<NoteCardQueryDto> FeaturedNotes { get; set; } = null!;
}