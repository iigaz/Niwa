using System.ComponentModel.DataAnnotations;
using Niwa.Models.Meta;

namespace Niwa.Dtos.CollectionDtos;

public class ChangeCollectionCommandDto
{
    public string CollectionId { get; set; } = "";

    public bool ShouldCreateNewCollection { get; set; }

    [Length(Lengths.CollectionTitleMin, Lengths.CollectionTitleMax)]
    public string NewCollectionTitle { get; set; } = "";
}