namespace CommandsService.Models;

public class Platform
{
    public int Id { get; init; }

    public int ExternalId { get; init; }

    public string Name { get; init; } = null!;

    public ICollection<Command> Commands { get; init; } = new List<Command>();
}
