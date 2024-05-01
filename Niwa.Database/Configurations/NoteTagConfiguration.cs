using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Niwa.Models;

namespace Niwa.Database.Configurations;

public class NoteTagConfiguration : IEntityTypeConfiguration<NoteTag>
{
    public void Configure(EntityTypeBuilder<NoteTag> builder)
    {
        builder.HasOne(noteTag => noteTag.Note).WithMany(note => note.Tags);
    }
}