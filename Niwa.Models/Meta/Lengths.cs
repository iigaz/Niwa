namespace Niwa.Models.Meta;

public static class Lengths
{
    public const int UsernameMin = 2;
    public const int UsernameMax = 32;

    public const int EmailAddressMin = 0;
    public const int EmailAddressMax = 256;

    public const int PasswordHashMin = 0;
    public const int PasswordHashMax = 128;

    public const int RoleLabelMin = 0;
    public const int RoleLabelMax = 32;

    public const int GardenTitleMin = 2;
    public const int GardenTitleMax = 64;

    public const int GardenSummaryMin = NoteSummaryMin;
    public const int GardenSummaryMax = NoteSummaryMax;

    public const int NoteTitleMin = 2;
    public const int NoteTitleMax = 256;

    public const int NoteSummaryMin = 0;
    public const int NoteSummaryMax = 2048;

    public const int NoteContentMin = 0;
    public const int NoteContentMax = 65536;

    public const int CollectionTitleMin = 0;
    public const int CollectionTitleMax = 64;

    public const int UrlMin = 0;
    public const int UrlMax = 2048;

    public const int CommentContentMin = 1;
    public const int CommentContentMax = 2048;

    public const int TagMin = 2;
    public const int TagMax = 32;

    public const int FilenameMin = 4;
    public const int FilenameMax = 512;

    public const int ShortIdMin = 1;
    public const int ShortIdMax = 10;
}