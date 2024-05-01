using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Niwa.Models;

namespace Niwa.Database.Configurations;

public class CollectionConfiguration : IEntityTypeConfiguration<Collection>
{
    public void Configure(EntityTypeBuilder<Collection> builder)
    {
        builder
            .HasOne(collection => collection.User)
            .WithMany();
        builder
            .HasMany(collection => collection.Notes)
            .WithMany(notes => notes.Collections);
    }
}