namespace PlatformService.Persistence.Repository;

public class PlatformRepository : IPlatformRepository
{
    private readonly ApplicationDbContext _context;

    public PlatformRepository(ApplicationDbContext context) =>
        _context = context;

    public bool SaveChanges() =>
        _context.SaveChanges() >= 0;

    public void Create(Platform platform) =>
         _context.Platforms.Add(platform);

    public IEnumerable<Platform> AllPlatforms() =>
        _context.Platforms;

    public Platform? PlatformBy(int id) =>
        _context.Platforms.FirstOrDefault(p => p.Id == id);
}
