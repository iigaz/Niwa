namespace Niwa.Services.SubscriptionServices;

public interface ISubscriptionCommandService
{
    public Task<bool> SubscribeToNote(Guid userId, Guid noteId, bool subscribe = true);
    public Task<bool> SubscribeToGarden(Guid userId, Guid gardenId, bool subscribe = true);
}