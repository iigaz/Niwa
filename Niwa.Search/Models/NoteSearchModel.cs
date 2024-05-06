using Niwa.Models;
using Niwa.Models.Enums;
using OpenSearch.Client;

namespace Niwa.Search.Models;

public class NoteSearchModel
{
    public Guid Id { get; set; }

    [Keyword]
    public string ShortId { get; set; } = null!;

    [Keyword]
    public string Author { get; set; } = null!;

    public Guid AuthorId { get; set; }

    public string Title { get; set; } = null!;

    public string Summary { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string GardenTitle { get; set; } = null!;

    [Keyword]
    public string? Image { get; set; }

    public Access Access { get; set; }

    [Keyword]
    public ICollection<string> Tags { get; set; } = [];

    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }

    public static NoteSearchModel From(Note note)
    {
        return new NoteSearchModel
        {
            Id = note.Id,
            ShortId = note.ShortId,
            Author = note.User.Username,
            AuthorId = note.UserId,
            Title = note.Title,
            Summary = note.Summary,
            Content = note.Content,
            GardenTitle = note.Garden.Title,
            Image = note.Image,
            Access = note.Access,
            Tags = note.Tags.Select(tag => tag.Tag)
                .ToList(),
            Created = note.CreatedDateTime,
            Updated = note.LatestRevision.CreatedDateTime
        };
    }
}