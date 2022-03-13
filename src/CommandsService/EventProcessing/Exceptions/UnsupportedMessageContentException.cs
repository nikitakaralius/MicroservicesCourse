namespace CommandsService.EventProcessing.Exceptions;

public class UnsupportedMessageContentException : Exception
{
    public UnsupportedMessageContentException(EventType eventType, string message)
        : base($"Message {message} does not correspond to the {eventType} event type") { }
}
