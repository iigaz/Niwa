using Microsoft.EntityFrameworkCore;
using Niwa.Models;
using Niwa.Models.Enums;
using Niwa.Services.NoteRepositories;
using Niwa.Services.UserRepositories;

namespace Niwa.Services.NoteServices;

public class NoteQueryService(INoteQueryRepository noteQueryRepository, IUserQueryRepository userQueryRepository)
    : INoteQueryService
{
    public Task<Note?> GetNoteByUsernameAndShortIdAsync(string username, string shortId)
    {
        return noteQueryRepository.GetNoteWithRelevantInfoByUsernameAndShortIdAsync(username, shortId);
    }

    public Task<int> GetCommentCountAsync(Note note)
    {
        return noteQueryRepository.GetCommentCountAsync(note.Id);
    }

    public async Task<bool> IsUserSubscribedAsync(Guid userId, Note note)
    {
        var user = await userQueryRepository.GetUserByIdWithSubscribedNotesAsync(userId);
        return user != null && user.SubscribedNotes.Any(n => n.Id == note.Id);
    }

    public Task<Note?> GetNoteSnapshotAsync(Note note, Guid startingRevisionId)
    {
        return noteQueryRepository.GetNoteSnapshotAsync(note, startingRevisionId);
    }

    public Task<List<NoteRevision>> GetNoteRevisionsAsync(Guid noteId)
    {
        return noteQueryRepository.GetNoteRevisionsAsync(noteId);
    }

    public async Task<List<Note>> GetNotesByTagAsync(Guid currentUser, string tag)
    {
        return (await noteQueryRepository.GetNotesByTagAsync(tag))
            .Where(note => note.Access == Access.Public || note.UserId == currentUser).ToList();
    }

    public Task<List<Note>> GetNotesAsync(Guid currentUser)
    {
        return noteQueryRepository.GetNotesWithTagsGardenAndUsers()
            .Where(note => note.Access == Access.Public || note.UserId == currentUser).ToListAsync();
    }
}