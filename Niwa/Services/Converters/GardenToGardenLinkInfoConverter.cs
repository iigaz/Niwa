using Niwa.Dtos.GardenDtos;
using Niwa.Models;

namespace Niwa.Services.Converters;

public class GardenToGardenLinkInfoConverter : IGardenToGardenLinkInfoConverter
{
    public GardenLinkInfoQueryDto Convert(Garden garden)
    {
        return new GardenLinkInfoQueryDto
        {
            Title = garden.Title,
            AuthorUsername = garden.User.Username
        };
    }
}