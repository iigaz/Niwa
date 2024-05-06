using Niwa.Models;

namespace Niwa.Services;

public interface ILinkManager
{
    public string LinkToGarden(string username);
    public string LinkToNewNote(string username, string variant);
    public string LinkToNote(Note note);
    public string LinkToNote(string authorUsername, string shortId, string title);
    public string LinkToTag(string tag);
    public string LinkToCollection(Guid id);
    public string LinkToSnapshot(Note note, string snapshotId);
    public string LinkToHistory(Note note);
}