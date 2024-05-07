using Niwa.Models;
using Niwa.Services.NewsServices.Models;

namespace Niwa.Services.NewsServices;

public interface INewsQueryService
{
    public Task<List<NewsModel>> GetNewsAsync(Guid userId, int limit);
    public string GetNoteRevisionDescription(NoteRevision revision);
}