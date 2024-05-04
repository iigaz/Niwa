using Niwa.Dtos.CollectionDtos;
using Niwa.Models;

namespace Niwa.Services.Converters;

public class CollectionToCollectionConverter : ICollectionToCollectionConverter
{
    public CollectionQueryDto? Convert(Collection? collection)
    {
        return collection == null
            ? null
            : new CollectionQueryDto
            {
                Id = collection.Id,
                Title = collection.Title
            };
    }
}