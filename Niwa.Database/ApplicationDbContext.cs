using Microsoft.EntityFrameworkCore;
using Niwa.Database.Configurations;
using Niwa.Models;

namespace Niwa.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Collection> Collections { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Garden> Gardens { get; set; } = null!;
    public DbSet<Note> Notes { get; set; } = null!;
    public DbSet<NoteFile> NoteFiles { get; set; } = null!;
    public DbSet<NoteRevision> NoteRevisions { get; set; } = null!;
    public DbSet<NoteTag> NoteTags { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CollectionConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new GardenConfiguration());
        modelBuilder.ApplyConfiguration(new NoteConfiguration());
        modelBuilder.ApplyConfiguration(new NoteFileConfiguration());
        modelBuilder.ApplyConfiguration(new NoteRevisionConfiguration());
        modelBuilder.ApplyConfiguration(new NoteTagConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}