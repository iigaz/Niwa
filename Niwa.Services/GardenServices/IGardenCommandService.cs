namespace Niwa.Services.GardenServices;

public interface IGardenCommandService
{
    public Task UpdateAsync(Guid id, string title, string summary);
    public Task FeatureNote(Guid id, Guid noteId, bool feature = true);
}