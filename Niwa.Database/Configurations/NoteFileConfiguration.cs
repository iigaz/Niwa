using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Niwa.Models;

namespace Niwa.Database.Configurations;

public class NoteFileConfiguration : IEntityTypeConfiguration<NoteFile>
{
    public void Configure(EntityTypeBuilder<NoteFile> builder)
    {
        builder.HasOne(noteFile => noteFile.Note).WithMany(note => note.Files);
    }
}