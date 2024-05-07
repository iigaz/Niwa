using System.ComponentModel.DataAnnotations;
using Niwa.Models.Enums;
using Niwa.Models.Meta;

namespace Niwa.Models;

public class NoteRevision
{
    public Guid Id { get; set; }

    public Note Note { get; set; } = null!;

    /// <summary>
    ///     Changes in the title. Null if title was not changed since last revision.
    ///     If it is actually more efficient to store the whole title (instead of the delta),
    ///     then it stores the whole title.
    ///     For reference on whether it stores the full title or only the delta, see <see cref="TitleRewritten" />.
    /// </summary>
    [Length(Lengths.NoteTitleMin, Lengths.NoteTitleMax)]
    public string? TitleDelta { get; set; }

    /// <summary>
    ///     True if <see cref="TitleDelta" /> stores the full title, not only the delta.
    ///     False if <see cref="TitleDelta" /> only stores the delta.
    /// </summary>
    public bool TitleRewritten { get; set; } = false;

    /// <summary>
    ///     Changes in the summary. Null if summary was not changed since last revision.
    ///     If it is actually more efficient to store the whole summary (instead of the delta),
    ///     then it stores the whole summary.
    ///     For reference on whether it stores the full summary or only the delta, see <see cref="SummaryRewritten" />.
    /// </summary>
    [Length(Lengths.NoteSummaryMin, Lengths.NoteSummaryMax)]
    public string? SummaryDelta { get; set; }

    /// <summary>
    ///     True if <see cref="SummaryDelta" /> stores the full summary, not only the delta.
    ///     False if <see cref="SummaryDelta" /> only stores the delta.
    /// </summary>
    public bool SummaryRewritten { get; set; } = false;

    /// <summary>
    ///     Current access type. Null if it was not changed since last revision.
    /// </summary>
    public Access? Access { get; set; }

    /// <summary>
    ///     Changes in the content. Null if content was not changed since last revision.
    ///     If it is actually more efficient to store the whole content (instead of the delta),
    ///     then it stores the whole content.
    ///     For reference on whether it stores the full content or only the delta, see <see cref="ContentRewritten" />.
    /// </summary>
    [Length(Lengths.NoteContentMin, Lengths.NoteContentMax)]
    public string? ContentDelta { get; set; }

    /// <summary>
    ///     True if <see cref="ContentDelta" /> stores the full content, not only the delta.
    ///     False if <see cref="ContentDelta" /> only stores the delta.
    /// </summary>
    public bool ContentRewritten { get; set; } = false;

    /// <summary>
    ///     Previous revision. Null if this is the first revision in a chain.
    /// </summary>
    public Guid? PreviousRevisionId { get; set; }

    /// <summary>
    ///     Navigation property for <see cref="PreviousRevisionId" />.
    /// </summary>
    public NoteRevision? PreviousRevision { get; set; }

    public DateTime CreatedDateTime { get; set; }
}