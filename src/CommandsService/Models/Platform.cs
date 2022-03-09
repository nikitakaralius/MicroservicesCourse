namespace CommandsService.Models;

public class Platform
{
    public int Id { get; init; }

    public int ExternalId { get; init; }

    public string Name { get; set; } = null!;
}
