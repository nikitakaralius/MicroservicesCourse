using System.Text.Json;
using static CommandsService.EventProcessing.EventType;

namespace CommandsService.EventProcessing;

public class EventProcessorRouter : IEventProcessor
{
    private readonly IReadOnlyDictionary<EventType, IEventProcessor> _processors;

    public EventProcessorRouter(IServiceScopeFactory scopeFactory, IMapper mapper) =>
        _processors = new Dictionary<EventType, IEventProcessor>
        {
            {PlatformPublished, new PlatformEventProcessor(mapper, scopeFactory)},
            {Undetermined, new UndeterminedEventProcessor()}
        };

    public void ProcessEvent(string message)
    {
        EventType @event = DetermineEvent(message);
        _processors[@event].ProcessEvent(message);
    }

    private EventType DetermineEvent(string message)
    {
        GenericEvent? eventType = JsonSerializer.Deserialize<GenericEvent>(message);
        if (eventType is null)
        {
            return Undetermined;
        }
        return eventType.Event switch
        {
            "Platform_Published" => PlatformPublished,
            _ => Undetermined
        };
    }
}
