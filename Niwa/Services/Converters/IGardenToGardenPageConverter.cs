using Niwa.Dtos.GardenDtos.Read;
using Niwa.Models;

namespace Niwa.Services.Converters;

public interface IGardenToGardenPageConverter
{
    public GardenPageDto Convert(Garden garden, bool onlyPublicNotes = true);
}