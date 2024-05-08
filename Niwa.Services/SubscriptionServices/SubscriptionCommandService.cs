using Microsoft.Extensions.Logging;
using Niwa.Models.Enums;
using Niwa.Services.GardenRepositories;
using Niwa.Services.NoteRepositories;
using Niwa.Services.UserRepositories;

namespace Niwa.Services.SubscriptionServices;

public class SubscriptionCommandService(
    ILogger<SubscriptionCommandService> logger,
    IUserCommandRepository userCommandRepository,
    IUserQueryRepository userQueryRepository,
    INoteQueryRepository noteQueryRepository,
    IGardenQueryRepository gardenQueryRepository) : ISubscriptionCommandService
{
    public async Task<bool> SubscribeToNoteAsync(Guid userId, Guid noteId, bool subscribe = true)
    {
        var user = await userQueryRepository.GetUserByIdWithSubscribedNotesAsync(userId);
        var note = await noteQueryRepository.GetNoteByIdAsync(noteId);
        if (user == null || note == null || (note.Access != Access.Public && note.UserId != user.Id))
        {
            logger.LogWarning(
                "Tried to (un)subscribe user (Id={userId}) to note (Id={noteId}), but failed.\nReason: either could not find user or note, or the user has no permission to view this note.",
                userId, noteId);
            return false;
        }


        var alreadySubscribed = user.SubscribedNotes.Contains(note);
        if (subscribe)
        {
            if (alreadySubscribed)
                logger.LogInformation("User (Id={userId}) tried to subscribe to note (Id={noteId}) twice.", userId,
                    noteId);
            else user.SubscribedNotes.Add(note);
        }
        else
        {
            if (alreadySubscribed) user.SubscribedNotes.Remove(note);
            else
                logger.LogInformation(
                    "User (Id={userId}) tried to unsubscribe from a not subscribed note (Id={noteId}).", userId,
                    noteId);
        }

        await userCommandRepository.UpdateAsync(user);
        return true;
    }

    public async Task<bool> SubscribeToGardenAsync(Guid userId, Guid gardenId, bool subscribe = true)
    {
        var user = await userQueryRepository.GetUserByIdWithSubscribedGardensAsync(userId);
        var garden = await gardenQueryRepository.GetByIdAsync(gardenId);
        if (user == null || garden == null)
        {
            logger.LogWarning(
                "Tried to (un)subscribe user (Id={userId}) to garden (Id={gardenId}), but failed.\nReason: could not find user or garden.",
                userId, gardenId);
            return false;
        }

        var alreadySubscribed = user.SubscribedGardens.Contains(garden);
        if (subscribe)
        {
            if (alreadySubscribed)
                logger.LogInformation("User (Id={userId}) tried to subscribe to garden (Id={gardenId}) twice.", userId,
                    gardenId);
            else user.SubscribedGardens.Add(garden);
        }
        else
        {
            if (alreadySubscribed) user.SubscribedGardens.Remove(garden);
            else
                logger.LogInformation(
                    "User (Id={userId}) tried to unsubscribe from a not subscribed garden (Id={gardenId}).", userId,
                    gardenId);
        }

        await userCommandRepository.UpdateAsync(user);
        return true;
    }
}