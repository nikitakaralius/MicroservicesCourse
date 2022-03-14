namespace CommandsService.Persistence.Repository;

public interface ICommandRepository
{
    bool SaveChanges();

    IEnumerable<Platform> AllPlatforms();
    bool PlatformExists(int id);
    bool ExternalPlatformsExists(int id);
    void Create(Platform platform);

    IEnumerable<Command> CommandsForPlatform(int platformId);
    Command? CommandBy(int platformId, int commandId);
    Command Create(int platformId, Command command);
}
