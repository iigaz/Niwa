using Niwa.Models;

namespace Niwa.Services.NoteRepositories;

public interface INoteCommandRepository
{
    public Task CreateAsync(Note note, NoteRevision noteRevision);
    public Task UpdateAsync(Note note, NoteRevision noteRevision);
    public Task UpdateAsync(Note note);
}