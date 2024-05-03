namespace Niwa.Services;

public interface IShortIdParser
{
    public DateTime? ShortIdToDateTime(string shortId);

    public string DateTimeToShortId(DateTime dateTime);
}