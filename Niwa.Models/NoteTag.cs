using System.ComponentModel.DataAnnotations;
using Niwa.Models.Meta;

namespace Niwa.Models;

public class NoteTag
{
    public int Id { get; set; }

    public Guid NoteId { get; set; }
    public Note Note { get; set; } = null!;

    [Length(Lengths.TagMin, Lengths.TagMax)]
    public string Tag { get; set; } = null!;
}