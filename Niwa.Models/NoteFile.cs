using System.ComponentModel.DataAnnotations;
using Niwa.Models.Meta;

namespace Niwa.Models;

public class NoteFile
{
    public int Id { get; set; }

    public Guid NoteId { get; set; }
    public Note Note { get; set; } = null!;

    [Length(Lengths.UrlMin, Lengths.UrlMax)]
    [Url]
    public string File { get; set; } = null!;
}