using System.Text;
using Microsoft.EntityFrameworkCore;
using Niwa.Extensions;
using Niwa.Models;
using Niwa.Services.NewsServices.Models;
using Niwa.Services.NoteRepositories;
using Niwa.Services.UserRepositories;

namespace Niwa.Services.NewsServices;

public class NewsQueryService(IUserQueryRepository userQueryRepository, INoteQueryRepository noteQueryRepository)
    : INewsQueryService
{
    public async Task<List<NewsModel>> GetNewsAsync(Guid userId, int limit)
    {
        var user = await userQueryRepository.GetUsers().Include(user => user.SubscribedGardens)
            .Include(user => user.SubscribedNotes).SingleOrDefaultAsync(user => user.Id == userId);
        if (user == null)
            return [];
        var revisions = await noteQueryRepository.GetSubscribedNotesRevisionsAsync(user, limit);
        var notes = await noteQueryRepository.GetSubscribedGardensNotesAsync(user, limit);
        var news = revisions.Select(revision => new NewsModel
            {
                DateTime = revision.CreatedDateTime,
                Information = GetNoteRevisionDescription(revision),
                Title = $"\"{revision.Note.Title}\" was edited.",
                AuthorUsername = revision.Note.User.Username,
                Image = revision.Note.Image
            })
            .ToList();
        news.AddRange(notes.Select(note => new NewsModel
        {
            DateTime = note.CreatedDateTime,
            Information = $"{note.Title}",
            Title = $"\"{note.Garden.Title}\" added a new note.",
            AuthorUsername = note.User.Username,
            Image = note.Image
        }));

        return news.OrderByDescending(model => model.DateTime).Take(limit).ToList();
    }

    public string GetNoteRevisionDescription(NoteRevision revision)
    {
        var changed = new List<string>();
        if (revision.TitleDelta != null)
            changed.Add("title");
        if (revision.SummaryDelta != null)
            changed.Add("summary");
        if (revision.ContentDelta != null)
            changed.Add("content");
        if (revision.Access != null)
            changed.Add($"access ({revision.Access.GetName()})");
        var result = new StringBuilder();
        if (changed.Count == 0)
        {
            result.Append("Nothing changed.");
        }
        else if (changed.Count == 1)
        {
            result.Append($"Changed {changed[0]}.");
        }
        else
        {
            result.Append($"Changed {changed[0]}");
            for (var i = 1; i < changed.Count - 1; i++) result.Append($", {changed[i]}");

            result.Append($" and {changed[^1]}.");
        }

        return result.ToString();
    }
}