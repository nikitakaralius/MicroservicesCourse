namespace PlatformService.Models.External;

public class PlatformToPublish
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public string Event { get; init; } = null!;
}
