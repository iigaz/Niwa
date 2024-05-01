using System.ComponentModel.DataAnnotations;
using Niwa.Models.Meta;

namespace Niwa.Models;

public class Comment
{
    public Guid Id { get; set; }

    /// <summary>
    ///     Comment author.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    ///     Navigation property for <see cref="UserId" />.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    ///     Note, under which the comment was left.
    /// </summary>
    public Guid NoteId { get; set; }

    /// <summary>
    ///     Navigation property for <see cref="NoteId" />.
    /// </summary>
    public Note Note { get; set; } = null!;

    /// <summary>
    ///     Contents of the comment.
    /// </summary>
    [Length(Lengths.CommentContentMin, Lengths.CommentContentMax)]
    public string Content { get; set; } = null!;

    /// <summary>
    ///     Nothing ever gets truly deleted.
    /// </summary>
    public bool Deleted { get; set; } = false;

    /// <summary>
    ///     The comment to which this comment is a reply.
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    ///     Navigation property for <see cref="ParentId" />.
    /// </summary>
    public Comment? Parent { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public DateTime UpdatedDateTime { get; set; }
}