using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Niwa.Models;

namespace Niwa.Database.Configurations;

public class GardenConfiguration : IEntityTypeConfiguration<Garden>
{
    public void Configure(EntityTypeBuilder<Garden> builder)
    {
        builder.HasOne(garden => garden.User).WithMany(user => user.Gardens);
        builder.HasMany(garden => garden.Subscribers).WithMany(user => user.SubscribedGardens);
        builder.HasMany(garden => garden.Notes).WithOne(note => note.Garden);
    }
}