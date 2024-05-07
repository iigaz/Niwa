using Niwa.Models;

namespace Niwa.Dtos.FileDtos;

public class NoteFileQueryDto
{
    public string Filename { get; set; } = null!;
    public string FileUrl { get; set; } = null!;
    public NoteFile OriginalNoteFile { get; set; } = null!;
}