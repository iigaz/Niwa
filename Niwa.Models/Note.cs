using System.ComponentModel.DataAnnotations;
using Niwa.Models.Enums;
using Niwa.Models.Meta;
using Sqids;

namespace Niwa.Models;

public class Note
{
    public Guid Id { get; set; }

    [Length(Lengths.ShortIdMin, Lengths.ShortIdMax)]
    public string ShortId { get; set; } = null!;

    /// <summary>
    ///     Note author. Usually the same as in <see cref="Garden.UserId" />, but not necessarily always.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    ///     Navigation property for <see cref="UserId" />.
    /// </summary>
    public User User { get; set; } = null!;

    public Guid LatestRevisionId { get; set; }
    public NoteRevision LatestRevision { get; set; } = null!;

    /// <summary>
    ///     Garden which contains the note.
    /// </summary>
    public Guid GardenId { get; set; }

    /// <summary>
    ///     Navigation property for <see cref="GardenId" />.
    /// </summary>
    public Garden Garden { get; set; } = null!;

    /// <summary>
    ///     Fully assembled from the revisions latest version of the title. Used for fast access.
    /// </summary>
    [Length(Lengths.NoteTitleMin, Lengths.NoteTitleMax)]
    public string Title { get; set; } = null!;

    /// <summary>
    ///     Fully assembled from the revisions latest version of the summary. Used for fast access.
    /// </summary>
    [Length(Lengths.NoteSummaryMin, Lengths.NoteSummaryMax)]
    public string Summary { get; set; } = null!;

    /// <summary>
    ///     Fully assembled from the revisions latest version of the content. Used for fast access.
    /// </summary>
    [Length(Lengths.NoteContentMin, Lengths.NoteContentMax)]
    public string Content { get; set; } = null!;

    /// <summary>
    ///     Preview image, taken from the first image in the latest version of content (if any)
    /// </summary>
    [Length(Lengths.UrlMin, Lengths.UrlMax)]
    [Url]
    public string? Image { get; set; } = null!;

    /// <summary>
    ///     Current access type. Fully assembled from the revisions latest version of the content. Used for fast access.
    /// </summary>
    public Access Access { get; set; }

    public ICollection<Collection> Collections { get; set; } = new List<Collection>();

    public ICollection<NoteTag> Tags { get; set; } = new List<NoteTag>();

    public ICollection<NoteFile> Files { get; set; } = new List<NoteFile>();

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public ICollection<User> Subscribers { get; set; } = new List<User>();

    public DateTime CreatedDateTime { get; set; }

    public void GenerateShortId(string alphabet)
    {
        ShortId = new SqidsEncoder<long>(new SqidsOptions { Alphabet = alphabet }).Encode(
            ((DateTimeOffset)CreatedDateTime).ToUnixTimeSeconds());
        // TODO: move into a service
    }
}