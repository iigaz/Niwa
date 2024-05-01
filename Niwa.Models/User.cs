using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
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
    ///     Gardens that user has created.
    /// </summary>
    public ICollection<Garden> Gardens { get; set; } = new List<Garden>();

    /// <summary>
    ///     Notes that user has created.
    /// </summary>
    public ICollection<Note> Notes { get; set; } = new List<Note>();

    /// <summary>
    ///     Comments that user has created.
    /// </summary>
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

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

    private static string HashPasswordWithSalt(string password, byte[] salt)
    {
        var bytes = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100000, HashAlgorithmName.SHA256, 256 / 8);
        var hashed = Convert.ToBase64String(bytes);
        var saltString = Convert.ToBase64String(salt);
        var salted = hashed + ';' + saltString;
        return salted;
    }

    public static string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
        return HashPasswordWithSalt(password, salt);
    }

    public static bool CheckPassword(string hashedPassword, string password)
    {
        var splat = hashedPassword.Split(';');
        var saltString = splat[1];
        var salt = Convert.FromBase64String(saltString);
        var actualHashed = HashPasswordWithSalt(password, salt);
        return hashedPassword == actualHashed;
    }
}