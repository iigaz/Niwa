using Microsoft.EntityFrameworkCore;
using Niwa.Services.GardenRepositories;
using Niwa.Services.NoteRepositories;

namespace Niwa.Services.GardenServices;

public class GardenCommandService(
    IGardenCommandRepository gardenCommandRepository,
    IGardenQueryRepository gardenQueryRepository,
    INoteQueryRepository noteQueryRepository) : IGardenCommandService
{
    public async Task UpdateAsync(Guid id, string title, string summary)
    {
        var garden = await gardenQueryRepository.GetByIdAsync(id);
        if (garden == null)
            throw new ArgumentNullException();
        garden.Title = title;
        garden.Summary = summary;
        await gardenCommandRepository.UpdateAsync(garden);
    }

    public async Task FeatureNote(Guid id, Guid noteId, bool feature = true)
    {
        var garden = await gardenQueryRepository.GetGardens().Include(g => g.FeaturedNotes)
            .SingleOrDefaultAsync(garden => garden.Id == id);
        var note = await noteQueryRepository.GetNotes().SingleOrDefaultAsync(note => note.Id == noteId);
        if (garden == null || note == null)
            throw new ArgumentNullException();
        var alreadyFeatured = garden.FeaturedNotes.Any(n => n.Id == note.Id);
        if (feature && !alreadyFeatured)
            garden.FeaturedNotes.Add(note);
        else if (!feature && alreadyFeatured) garden.FeaturedNotes.Remove(note);

        await gardenCommandRepository.UpdateAsync(garden);
    }
}