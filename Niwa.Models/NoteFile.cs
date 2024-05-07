using System.ComponentModel.DataAnnotations;
using Niwa.Models.Meta;

namespace Niwa.Models;

public class NoteFile
{
    public int Id { get; set; }

    public Guid NoteId { get; set; }
    public Note Note { get; set; } = null!;

    [Length(Lengths.FilenameMin, Lengths.FilenameMax)]
    public string Filename { get; set; } = null!;

    [Length(Lengths.UrlMin, Lengths.UrlMax)]
    public string FileUrl { get; set; } = null!;
}