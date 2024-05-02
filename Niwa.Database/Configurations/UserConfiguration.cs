using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Niwa.Models;

namespace Niwa.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasAlternateKey(user => user.Username);
        builder.HasIndex(user => user.EmailAddress).IsUnique();
        builder.HasMany(user => user.Gardens).WithOne(garden => garden.User);
        builder.HasMany(user => user.Notes).WithOne(note => note.User);
        builder.HasMany(user => user.Comments).WithOne(note => note.User);
        builder.HasMany(user => user.Roles).WithMany(role => role.Users);
        builder.HasMany(user => user.SubscribedGardens).WithMany(garden => garden.Subscribers);
        builder.HasMany(user => user.SubscribedNotes).WithMany(note => note.Subscribers);
        builder.HasData(new User
        {
            Id = Guid.Parse("aaf91a62-1964-46c6-ab36-a95af1486272"),
            Username = "admin",
            EmailAddress = null,
            PasswordHash = User.HashPassword("admin"),
            CreatedDateTime = DateTime.UnixEpoch,
            UpdatedDateTime = DateTime.UnixEpoch
        });
    }
}