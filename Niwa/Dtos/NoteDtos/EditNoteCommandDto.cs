using System.ComponentModel.DataAnnotations;
using Niwa.Dtos.FileDtos;
using Niwa.Models.Enums;
using Niwa.Models.Meta;

namespace Niwa.Dtos.NoteDtos;

public class EditNoteCommandDto
{
    [Length(Lengths.NoteTitleMin, Lengths.NoteTitleMax)]
    public string Title { get; set; } = "";

    [Length(Lengths.NoteSummaryMin, Lengths.NoteSummaryMax)]
    public string Summary { get; set; } = "";

    [Length(Lengths.NoteContentMin, Lengths.NoteContentMax)]
    public string Content { get; set; } = "";

    public Access Access { get; set; }

    public string Tags { get; set; } = "";

    public ICollection<NoteFileQueryDto> Files { get; set; } = new List<NoteFileQueryDto>();
}