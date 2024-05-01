using System.ComponentModel.DataAnnotations;
using Niwa.Models.Meta;

namespace Niwa.Models;

public class Garden
{
    public Guid Id { get; set; }


    [Length(Lengths.GardenTitleMin, Lengths.GardenTitleMax)]
    public string Title { get; set; } = null!;

    /// <summary>
    ///     Author of the garden.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    ///     Navigation property for <see cref="UserId" />.
    /// </summary>
    public User User { get; set; } = null!;

    [Length(Lengths.GardenSummaryMin, Lengths.GardenSummaryMax)]
    public string Summary { get; set; } = "";

    public DateTime CreatedDateTime { get; set; }

    public DateTime UpdatedDateTime { get; set; }
}