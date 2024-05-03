using Niwa.Models;

namespace Niwa.Services;

public interface ILinkManager
{
    public string LinkToGarden(Garden garden);
    public string LinkToNote(Note note);
}