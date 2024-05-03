using Niwa.Dtos.NoteDtos.Read;
using Niwa.Models;

namespace Niwa.Services.Converters;

public interface INoteToNoteCardConverter
{
    public NoteCardDto Convert(Note note);
}