using Niwa.Dtos.NoteDtos;
using Niwa.Models;

namespace Niwa.Services.Converters;

public class NoteToNoteCardConverter(ILinkManager linkManager, ICollectionToCollectionConverter collectionConverter)
    : INoteToNoteCardConverter
{
    public NoteCardQueryDto Convert(Note note)
    {
        return new NoteCardQueryDto
        {
            Title = note.Title,
            Image = note.Image,
            Garden = note.Garden.Title,
            Summary = note.Summary,
            Access = note.Access,
            Tags = note.Tags.Select(tag => tag.Tag)
                .ToList(),
            Url = linkManager.LinkToNote(note)
        };
    }
}