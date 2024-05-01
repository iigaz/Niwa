using System.ComponentModel.DataAnnotations;
using Niwa.Models.Meta;

namespace Niwa.Models;

public class Collection
{
    public Guid Id { get; set; }

    /// <summary>
    ///     Collection author and the only editor and viewer.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    ///     Navigation property for <see cref="UserId" />.
    /// </summary>
    public User User { get; set; } = null!;

    [Length(Lengths.CollectionTitleMin, Lengths.CollectionTitleMax)]
    public string Title { get; set; } = null!;
    
    public ICollection<Note> Notes { get; set; } = new List<Note>();

    public DateTime CreatedDateTime { get; set; }

    public DateTime UpdatedDateTime { get; set; }

}