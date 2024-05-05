using Niwa.Models;
using Niwa.Services.NoteServices.Models;

namespace Niwa.Services.NoteServices;

public interface INoteCommandService
{
    public Task<Note?> CreateAsync(CreateNoteCommand noteCommand);
    public Task<bool> UpdateAsync(UpdateNoteCommand noteCommand);
}