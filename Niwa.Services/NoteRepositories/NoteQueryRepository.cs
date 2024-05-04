using Niwa.Database;
using Niwa.Models;

namespace Niwa.Services.NoteRepositories;

public class NoteQueryRepository(ApplicationDbContext context) : INoteQueryRepository
{
    public IQueryable<Note> GetNotes()
    {
        return context.Notes;
    }
}