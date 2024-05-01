using System.ComponentModel.DataAnnotations;
using Niwa.Models.Meta;

namespace Niwa.Models;

public class User
{
    public Guid Id { get; set; }

    /// <summary>
    ///     Unique username.
    /// </summary>
    [Length(Lengths.UsernameMin, Lengths.UsernameMax)]
    public string Username { get; set; } = null!;

    /// <summary>
    ///     Email address. Optional. User can register without an email address.
    /// </summary>
    [Length(Lengths.EmailAddressMin, Lengths.EmailAddressMax)]
    [EmailAddress]
    public string? EmailAddress { get; set; }

    [Length(Lengths.PasswordHashMin, Lengths.PasswordHashMax)]
    public string PasswordHash { get; set; } = null!;

    public ICollection<Role> Roles { get; set; } = new List<Role>();

    /// <summary>
    ///     Notes user subscribed to.
    /// </summary>
    public ICollection<Note> SubscribedNotes { get; set; } = new List<Note>();

    /// <summary>
    ///     Gardens user subscribed to.
    /// </summary>
    public ICollection<Garden> SubscribedGardens { get; set; } = new List<Garden>();

    public DateTime CreatedDateTime { get; set; }

    public DateTime UpdatedDateTime { get; set; }
}