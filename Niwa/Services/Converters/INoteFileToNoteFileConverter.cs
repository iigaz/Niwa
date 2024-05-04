using Niwa.Dtos.FileDtos;
using Niwa.Models;

namespace Niwa.Services.Converters;

public interface INoteFileToNoteFileConverter
{
    public NoteFileQueryDto Convert(NoteFile noteFile);
}