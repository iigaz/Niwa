using Niwa.Models;

namespace Niwa.Services;

public interface ILinkManager
{
    public string LinkToGarden(string username);
    public string LinkToNewNote(string username, string variant);
    public string LinkToNote(Note note);
    public string LinkToTag(string tag);
    public string LinkToCollection(Guid id);
}