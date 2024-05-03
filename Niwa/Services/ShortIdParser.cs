using Sqids;

namespace Niwa.Services;

public class ShortIdParser : IShortIdParser
{
    public ShortIdParser(IConfiguration configuration)
    {
        SqidsEncoder = new SqidsEncoder<long>(new SqidsOptions
        {
            Alphabet = configuration["Sqids:Alphabet"] ??
                       "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        });
    }

    private SqidsEncoder<long> SqidsEncoder { get; }

    public DateTime? ShortIdToDateTime(string shortId)
    {
        return SqidsEncoder.Decode(shortId) is [var dateData]
            ? DateTimeOffset.FromUnixTimeSeconds(dateData).DateTime
            : null;
    }

    public string DateTimeToShortId(DateTime dateTime)
    {
        return SqidsEncoder.Encode(((DateTimeOffset)dateTime).ToUnixTimeSeconds());
    }
}