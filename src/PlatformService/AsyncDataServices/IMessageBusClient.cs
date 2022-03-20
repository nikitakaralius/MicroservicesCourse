namespace PlatformService.AsyncDataServices;

public interface IMessageBusClient
{
    void PublishNewPlatform(PlatformToPublish platform);
}
