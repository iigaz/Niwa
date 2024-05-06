using Niwa.Models;
using Niwa.Models.Enums;

namespace Niwa.Search.Models;

public class NoteSearchModel
{
    public Guid Id { get; set; }

    public string ShortId { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Summary { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string GardenTitle { get; set; } = null!;

    public string? Image { get; set; }

    public Access Access { get; set; }

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