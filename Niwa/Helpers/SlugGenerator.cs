using SluggyUnidecode;

namespace Niwa.Helpers;

public static class SlugGenerator
{
    public static string FromTitle(string title)
    {
        return title.ToSlug();
    }
}