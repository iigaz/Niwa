using Niwa.Dtos.NoteDtos;
using Niwa.Models;

namespace Niwa.Services.Converters;

public interface INoteToNotePageConverter
{
    public NotePageQueryDto Convert(Note note, int commentCount, Collection? collection, bool featured);
}