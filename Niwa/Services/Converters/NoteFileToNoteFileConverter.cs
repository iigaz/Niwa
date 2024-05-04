using Niwa.Dtos.FileDtos;
using Niwa.Models;

namespace Niwa.Services.Converters;

public class NoteFileToNoteFileConverter : INoteFileToNoteFileConverter
{
    public NoteFileQueryDto Convert(NoteFile noteFile)
    {
        return new NoteFileQueryDto
        {
            Filename = noteFile.Filename,
            FileUrl = noteFile.FileUrl
        };
    }
}