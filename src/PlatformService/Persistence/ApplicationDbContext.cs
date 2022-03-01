using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Platform> Platforms { get; init; } = null!;
}
