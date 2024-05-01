using System.ComponentModel.DataAnnotations;
using Niwa.Models.Meta;

namespace Niwa.Models;

public class Role
{
    public int Id { get; set; }

    /// <summary>
    ///     User-readable role label.
    /// </summary>
    [Length(Lengths.RoleLabelMin, Lengths.RoleLabelMax)]
    public string Label { get; set; } = null!;

    /// <summary>
    ///     Users who have this role.
    /// </summary>
    public ICollection<User> Users { get; set; } = new List<User>();
}