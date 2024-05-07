using Niwa.Dtos.NoteDtos;
using Niwa.Models;

namespace Niwa.Services.Converters;

public class NoteToNotePageConverter(
    IGardenToGardenLinkInfoConverter gardenLinkInfoConverter,
    ICollectionToCollectionConverter collectionConverter)
    : INoteToNotePageConverter
{
    public NotePageQueryDto Convert(Note note, int commentCount, Collection? collection, bool featured)
    {
        return new NotePageQueryDto
        {
            Title = note.Title,
            Access = note.Access,
            Garden = gardenLinkInfoConverter.Convert(note.Garden),
            Summary = note.Summary,
            Content = note.Content,
            Attachments = [],
            Tags = note.Tags.Select(tag => tag.Tag).ToList(),
            LatestUpdateDateTime = note.LatestRevision.CreatedDateTime,
            CommentCount = commentCount,
            Collection = collectionConverter.Convert(collection),
            Featured = featured
        };
    }
}