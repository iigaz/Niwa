using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Niwa.Models;

namespace Niwa.Database.Configurations;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasOne(note => note.User).WithMany(user => user.Notes);
        builder.HasOne(note => note.Garden).WithMany(garden => garden.Notes);
        builder.HasOne(note => note.LatestRevision).WithOne(revision => revision.Note);
        builder.HasMany(note => note.Tags).WithOne(tag => tag.Note);
        builder.HasMany(note => note.Files).WithOne(file => file.Note);
        builder.HasMany(note => note.Comments).WithOne(comment => comment.Note);
        builder.HasMany(note => note.Collections).WithMany(collection => collection.Notes);
        builder.HasMany(note => note.Subscribers).WithMany(user => user.SubscribedNotes);
    }
}