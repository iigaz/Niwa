using Microsoft.EntityFrameworkCore;
using Niwa.Models.Enums;
using Niwa.Services.GardenRepositories;
using Niwa.Services.NoteRepositories;
using Niwa.Services.UserRepositories;

namespace Niwa.Services.SubscriptionServices;

public class SubscriptionCommandService(
    IUserCommandRepository userCommandRepository,
    IUserQueryRepository userQueryRepository,
    INoteQueryRepository noteQueryRepository,
    IGardenQueryRepository gardenQueryRepository) : ISubscriptionCommandService
{
    public async Task<bool> SubscribeToNote(Guid userId, Guid noteId, bool subscribe = true)
    {
        var user = await userQueryRepository.GetUsers().Include(user => user.SubscribedNotes)
            .SingleOrDefaultAsync(user => user.Id == userId);
        var note = await noteQueryRepository.GetNotes().SingleOrDefaultAsync(note => note.Id == noteId);
        if (user == null || note == null || (note.Access != Access.Public && note.UserId != user.Id))
            return false;
        var alreadySubscribed = user.SubscribedNotes.Contains(note);
        if (subscribe && !alreadySubscribed)
            user.SubscribedNotes.Add(note);
        else if (!subscribe && alreadySubscribed)
            user.SubscribedNotes.Remove(note);
        await userCommandRepository.UpdateAsync(user);
        return true;
    }

    public async Task<bool> SubscribeToGarden(Guid userId, Guid gardenId, bool subscribe = true)
    {
        var user = await userQueryRepository.GetUsers().Include(user => user.SubscribedGardens)
            .SingleOrDefaultAsync(user => user.Id == userId);
        var garden = await gardenQueryRepository.GetByIdAsync(gardenId);
        if (user == null || garden == null)
            return false;
        var alreadySubscribed = user.SubscribedGardens.Contains(garden);
        if (subscribe && !alreadySubscribed)
            user.SubscribedGardens.Add(garden);
        else if (!subscribe && alreadySubscribed)
            user.SubscribedGardens.Remove(garden);
        await userCommandRepository.UpdateAsync(user);
        return true;
    }
}