namespace PlatformService.Extensions;

public static class ConfigurationExtensions
{
    public static string CommandsService(this IConfiguration configuration) =>
        configuration["CommandsService"];

    public static string PlatformsConnectionString(this IConfiguration configuration) =>
        configuration.GetConnectionString("Platforms");
}
