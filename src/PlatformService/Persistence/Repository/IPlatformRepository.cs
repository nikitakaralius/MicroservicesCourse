namespace PlatformService.Persistence.Repository;

public interface IPlatformRepository
{
    bool SaveChanges();
    void Create(Platform platform);

    IEnumerable<Platform> AllPlatforms();
    Platform? PlatformBy(int id);
}
