using Niwa.Dtos.NoteDtos.Read;
using Niwa.Models;

namespace Niwa.Services.Converters;

public class NoteToNoteCardConverter(ILinkManager linkManager) : INoteToNoteCardConverter
{
    public NoteCardDto Convert(Note note)
    {
        return new NoteCardDto
        {
            Title = note.Title,
            Image = note.Image,
            Garden = note.Garden.Title,
            Summary = note.Summary,
            Access = note.Access,
            Tags = note.Tags.Select(tag => tag.Tag).ToList(),
            Url = linkManager.LinkToNote(note)
        };
    }
}