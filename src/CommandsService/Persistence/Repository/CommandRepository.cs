namespace CommandsService.Persistence.Repository;

public class CommandRepository : ICommandRepository
{
    private readonly ApplicationDbContext _context;

    public CommandRepository(ApplicationDbContext context) => _context = context;

    public bool SaveChanges() => _context.SaveChanges() >= 0;

    public IEnumerable<Platform> AllPlatforms() => _context.Platforms;

    public bool PlatformExists(int id) => _context.Platforms.Any(p => p.Id == id);
    public bool ExternalPlatformsExists(int id) =>
        _context.Platforms.Any(p => p.ExternalId == id);

    public void Create(Platform platform) => _context.Platforms.Add(platform);

    public IEnumerable<Command> CommandsForPlatform(int platformId) =>
        _context.Commands
            .Where(c => c.PlatformId == platformId)
            .OrderBy(c => c.Platform.Name.Length);

    public Command? CommandBy(int platformId, int commandId) =>
        _context.Commands
            .FirstOrDefault(c => c.PlatformId == platformId
                                 && c.Id == commandId);

    public void Create(int platformId, Command command) =>
        _context.Commands.Add(command with {PlatformId = platformId});
}
