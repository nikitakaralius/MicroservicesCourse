namespace CommandsService.Persistence.Repository;

public interface ICommandRepository
{
    bool SaveChanges();

    IEnumerable<Platform> AllPlatforms();
    bool PlatformExists(int id);
    void Create(Platform platform);

    IEnumerable<Command> CommandsForPlatform(int platformId);
    Command? CommandBy(int platformId, int commandId);
    void Create(int platformId, Command command);
}
