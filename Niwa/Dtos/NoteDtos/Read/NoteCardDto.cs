using Niwa.Models;
using Niwa.Models.Enums;

namespace Niwa.Dtos.NoteDtos.Read;

public class NoteCardDto
{
    public string Title { get; set; } = null!;

    public string? Image { get; set; }

    public string Garden { get; set; } = null!;

    public string Summary { get; set; } = null!;

    public Access Access { get; set; }

    public List<string> Tags { get; set; } = null!;

    public static NoteCardDto From(Note note)
    {
        return new NoteCardDto
        {
            Title = note.Title,
            Image = note.Image,
            Garden = note.Garden.Title,
            Summary = note.Summary,
            Access = note.Access
        };
    }
}