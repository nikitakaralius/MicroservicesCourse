namespace CommandsService.Models.Domain;

public record Command(int Id, string HowTo, string Line, int PlatformId, Platform Platform);