namespace PlatformService.Models.Domain;

public class Platform
{
    public Platform()
    {

    }

    public Platform(int id, string name, string publisher, string cost)
    {
        Id = id;
        Name = name;
        Publisher = publisher;
        Cost = cost;
    }

    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string Publisher { get; init; } = null!;
    public string Cost { get; init; } = null!;

    public void Deconstruct(out int id, out string name, out string publisher, out string cost)
    {
        id = Id;
        name = Name;
        publisher = Publisher;
        cost = Cost;
    }
}
