using Niwa.Services.NoteServices.Models;

namespace Niwa.Services.NoteServices;

public interface INoteCommandService
{
    public Task CreateAsync(CreateNoteCommand noteCommand);
    public Task UpdateAsync(UpdateNoteCommand noteCommand);
}