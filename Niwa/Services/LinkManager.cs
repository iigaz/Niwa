using Niwa.Helpers;
using Niwa.Models;

namespace Niwa.Services;

public class LinkManager(IShortIdParser shortIdParser) : ILinkManager
{
    public string LinkToGarden(Garden garden)
    {
        return $"/garden/{garden.User.Username}";
    }

    public string LinkToNote(Note note)
    {
        return
            $"/garden/{note.User.Username}/{shortIdParser.DateTimeToShortId(note.CreatedDateTime)}/{SlugGenerator.FromTitle(note.Title)}";
    }
}