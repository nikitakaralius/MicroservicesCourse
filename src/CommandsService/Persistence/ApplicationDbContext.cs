using Microsoft.EntityFrameworkCore;

namespace CommandsService.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Command> Commands { get; init; } = null!;

    public DbSet<Platform> Platforms { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Platform>()
            .HasMany(platform => platform.Commands)
            .WithOne(command => command.Platform)
            .HasForeignKey(command => command.PlatformId);

        modelBuilder
            .Entity<Command>()
            .HasOne(command => command.Platform)
            .WithMany(platform => platform.Commands)
            .HasForeignKey(command => command.PlatformId);
    }
}
