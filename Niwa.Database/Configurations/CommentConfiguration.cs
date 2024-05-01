using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Niwa.Models;

namespace Niwa.Database.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasOne(comment => comment.User).WithMany(user => user.Comments);
        builder.HasOne(comment => comment.Note).WithMany(note => note.Comments);
        builder.HasOne(comment => comment.Parent).WithMany();
    }
}