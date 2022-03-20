using System.Text.Json;
using CommandsService.EventProcessing.Exceptions;

namespace CommandsService.EventProcessing;

public class PlatformEventProcessor : IEventProcessor
{
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;

    public PlatformEventProcessor(IMapper mapper, IServiceScopeFactory scopeFactory)
    {
        _mapper = mapper;
        _scopeFactory = scopeFactory;
    }

    public void ProcessEvent(string message)
    {
        using IServiceScope scope = _scopeFactory.CreateScope();
        ICommandRepository repository = scope.ServiceProvider.GetRequiredService<ICommandRepository>();
        PlatformToReceive? receivedPlatform = JsonSerializer.Deserialize<PlatformToReceive>(message);
        try
        {
            if (receivedPlatform is null)
            {
                throw new UnsupportedMessageContentException(EventType.PlatformPublished, message);
            }

            Platform platform = _mapper.Map<Platform>(receivedPlatform);

            if (repository.ExternalPlatformsExists(platform))
            {
                Console.WriteLine("==> Platform already exists");
                return;
            }

            repository.Create(platform);
            repository.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine($"==> Could not add platform to DB {e.Message}");
        }
    }
}
