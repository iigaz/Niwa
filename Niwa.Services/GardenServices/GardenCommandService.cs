using Microsoft.Extensions.Logging;
using Niwa.Services.GardenRepositories;
using Niwa.Services.NoteRepositories;

namespace Niwa.Services.GardenServices;

public class GardenCommandService(
    ILogger<GardenCommandService> logger,
    IGardenCommandRepository gardenCommandRepository,
    IGardenQueryRepository gardenQueryRepository,
    INoteQueryRepository noteQueryRepository) : IGardenCommandService
{
    public async Task<bool> UpdateAsync(Guid id, string title, string summary)
    {
        var garden = await gardenQueryRepository.GetByIdAsync(id);
        if (garden == null)
        {
            logger.LogWarning("Tried to update garden (Id={gardenId}), but could not find it.", id);
            return false;
        }

        garden.Title = title;
        garden.Summary = summary;
        await gardenCommandRepository.UpdateAsync(garden);
        return true;
    }

    public async Task<bool> FeatureNoteAsync(Guid id, Guid noteId, bool feature = true)
    {
        var garden = await gardenQueryRepository.GetGardenWithFeaturedNotesByIdAsync(id);
        var note = await noteQueryRepository.GetNoteByIdAsync(noteId);
        if (garden == null || note == null)
        {
            logger.LogWarning(
                "Tried to (un)feature note (Id={noteId}) in garden (Id={gardenId}), but could not find them.", noteId,
                id);
            return false;
        }

        var alreadyFeatured = garden.FeaturedNotes.Any(n => n.Id == note.Id);
        if (feature)
        {
            if (alreadyFeatured)
                logger.LogWarning("Tried to feature already featured note (Id={noteId})", noteId);
            else
                garden.FeaturedNotes.Add(note);
        }
        else
        {
            if (alreadyFeatured)
                garden.FeaturedNotes.Remove(note);
            else
                logger.LogWarning("Tried to stop featuring not featured note (Id={noteId})", noteId);
        }

        await gardenCommandRepository.UpdateAsync(garden);
        return true;
    }
}