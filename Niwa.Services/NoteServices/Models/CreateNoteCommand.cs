using System.ComponentModel.DataAnnotations;
using Niwa.Models;
using Niwa.Models.Enums;
using Niwa.Models.Meta;

namespace Niwa.Services.NoteServices.Models;

public class CreateNoteCommand
{
    public Guid UserId { get; set; }

    public Garden Garden { get; set; }

    [Length(Lengths.NoteTitleMin, Lengths.NoteTitleMax)]
    public string Title { get; set; } = null!;

    [Length(Lengths.NoteSummaryMin, Lengths.NoteSummaryMax)]
    public string Summary { get; set; } = null!;

    [Length(Lengths.NoteContentMin, Lengths.NoteContentMax)]
    public string Content { get; set; } = null!;

    public Access Access { get; set; }

    public ICollection<NoteTag> Tags { get; set; } = new List<NoteTag>();

    public ICollection<NoteFile> Files { get; set; } = new List<NoteFile>();
}