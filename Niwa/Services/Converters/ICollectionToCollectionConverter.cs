using Niwa.Dtos.CollectionDtos;
using Niwa.Models;

namespace Niwa.Services.Converters;

public interface ICollectionToCollectionConverter
{
    public CollectionQueryDto? Convert(Collection? collection);
}