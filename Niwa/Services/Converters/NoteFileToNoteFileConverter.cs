using Niwa.Dtos.FileDtos;
using Niwa.Models;

namespace Niwa.Services.Converters;

public class NoteFileToNoteFileConverter(IFileDownloadService fileDownloadService) : INoteFileToNoteFileConverter
{
    public async Task<NoteFileQueryDto> Convert(NoteFile noteFile)
    {
        return new NoteFileQueryDto
        {
            Filename = noteFile.Filename,
            FileUrl = await fileDownloadService.GetDownloadUrlAsync(noteFile),
            OriginalNoteFile = noteFile
        };
    }
}