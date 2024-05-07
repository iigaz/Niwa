using Niwa.Models;

namespace Niwa.Services;

public interface IFileDownloadService
{
    public Task<string> GetDownloadUrlAsync(NoteFile noteFile);
}