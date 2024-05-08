namespace Niwa.Services.GardenServices;

public interface IGardenCommandService
{
    public Task<bool> UpdateAsync(Guid id, string title, string summary);
    public Task<bool> FeatureNoteAsync(Guid id, Guid noteId, bool feature = true);
}