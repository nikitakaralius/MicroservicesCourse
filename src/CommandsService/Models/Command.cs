namespace CommandsService.Models;

public class Command
{
    public int Id { get; init; }

    public string HowTo { get; init; } = null!;

    public string Line { get; init; } = null!;

    public int PlatformId { get; init; }

    public Platform Platform { get; init; }
}
