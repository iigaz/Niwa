using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Niwa.Models;

namespace Niwa.Database.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasMany(role => role.Users).WithMany(user => user.Roles);
        builder.HasData(new Role
        {
            Id = 1,
            Label = "viewer"
        }, new Role
        {
            Id = 2,
            Label = "editor"
        }, new Role
        {
            Id = 3,
            Label = "commentator"
        });
    }
}