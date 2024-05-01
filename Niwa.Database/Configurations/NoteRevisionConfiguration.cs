using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Niwa.Models;

namespace Niwa.Database.Configurations;

public class NoteRevisionConfiguration : IEntityTypeConfiguration<NoteRevision>
{
    public void Configure(EntityTypeBuilder<NoteRevision> builder)
    {
        builder.HasOne(revision => revision.PreviousRevision).WithOne();
    }
}