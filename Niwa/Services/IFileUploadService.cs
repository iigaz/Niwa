using Niwa.Dtos.FileDtos;

namespace Niwa.Services;

public interface IFileUploadService
{
    public Task<List<NoteFileQueryDto>> UploadFilesAsync(IEnumerable<IFormFile> formFileCollection);
}