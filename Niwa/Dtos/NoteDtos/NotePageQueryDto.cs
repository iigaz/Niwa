using Niwa.Dtos.CollectionDtos;
using Niwa.Dtos.FileDtos;
using Niwa.Dtos.GardenDtos;
using Niwa.Models.Enums;

namespace Niwa.Dtos.NoteDtos;

public class NotePageQueryDto
{
    public string Title { get; set; } = null!;
    public Access Access { get; set; }
    public GardenLinkInfoQueryDto Garden { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public string Content { get; set; } = null!;
    public List<NoteFileQueryDto> Attachments { get; set; } = null!;
    public List<string> Tags { get; set; } = null!;
    public DateTime LatestUpdateDateTime { get; set; }
    public int CommentCount { get; set; }
    public CollectionQueryDto? Collection { get; set; }
    public bool Featured { get; set; }
}