using Niwa.Dtos.GardenDtos;
using Niwa.Models;

namespace Niwa.Services.Converters;

public interface IGardenToGardenLinkInfoConverter
{
    public GardenLinkInfoQueryDto Convert(Garden garden);
}