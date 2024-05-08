namespace Niwa.Services.SubscriptionServices;

public interface ISubscriptionCommandService
{
    public Task<bool> SubscribeToNoteAsync(Guid userId, Guid noteId, bool subscribe = true);
    public Task<bool> SubscribeToGardenAsync(Guid userId, Guid gardenId, bool subscribe = true);
}