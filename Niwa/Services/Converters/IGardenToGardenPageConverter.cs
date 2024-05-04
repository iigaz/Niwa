using Niwa.Dtos.GardenDtos;
using Niwa.Models;

namespace Niwa.Services.Converters;

public interface IGardenToGardenPageConverter
{
    public GardenPageQueryDto Convert(Garden garden, bool onlyPublicNotes = true);
}