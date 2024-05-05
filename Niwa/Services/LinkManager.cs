using Niwa.Helpers;
using Niwa.Models;

namespace Niwa.Services;

public class LinkManager : ILinkManager
{
    public string LinkToGarden(string username)
    {
        return $"/garden/{username}";
    }

    public string LinkToNewNote(string username, string variant)
    {
        return LinkToGarden(username) + "/new/" + variant;
    }

    public string LinkToNote(Note note)
    {
        return
            $"/garden/{note.User.Username}/{note.ShortId}/{SlugGenerator.FromTitle(note.Title)}";
    }

    public string LinkToTag(string tag)
    {
        return $"/tag/{tag}";
    }

    public string LinkToCollection(Guid id)
    {
        return $"/collection/{id}";
    }

    public string LinkToSnapshot(Note note, string snapshotId)
    {
        return LinkToNote(note) + "/snapshot/" + snapshotId;
    }

    public string LinkToHistory(Note note)
    {
        return LinkToNote(note) + "/history";
    }

    public string LinkToGarden(Garden garden)
    {
        return LinkToGarden(garden.User);
    }

    public string LinkToGarden(User user)
    {
        return LinkToGarden(user.Username);
    }
}