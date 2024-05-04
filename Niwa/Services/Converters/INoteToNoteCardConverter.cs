using Niwa.Dtos.NoteDtos;
using Niwa.Models;

namespace Niwa.Services.Converters;

public interface INoteToNoteCardConverter
{
    public NoteCardQueryDto Convert(Note note);
}