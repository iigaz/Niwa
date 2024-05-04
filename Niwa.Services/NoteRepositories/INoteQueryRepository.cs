using Niwa.Models;

namespace Niwa.Services.NoteRepositories;

public interface INoteQueryRepository
{
    public IQueryable<Note> GetNotes();
}