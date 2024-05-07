namespace Niwa.Services.NewsServices.Models;

public class NewsModel
{
    public DateTime DateTime { get; set; }
    public string Information { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? AuthorUsername { get; set; }
    public string? Image { get; set; }
}