namespace PlatformService.SyncDataServices;

public interface ICommandDataClient
{
    Task SendPlatformToCommandAsync(PlatformToRead platform);
}
