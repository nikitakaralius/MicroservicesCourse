namespace PlatformService.Extensions;

public static class ConfigurationExtensions
{
    public static string CommandsService(this IConfiguration configuration) =>
        configuration["CommandsService"];

    public static string PlatformsConnectionString(this IConfiguration configuration) =>
        configuration.GetConnectionString("Platforms");

    public static string RabbitMQHost(this IConfiguration configuration) =>
        configuration["RabbitMQHost"];

    public static int RabbitMQPort(this IConfiguration configuration) =>
        int.Parse(configuration["RabbitMQPort"]);

    public static string RabbitMQExchange(this IConfiguration configuration) =>
        configuration["RabbitMQExchange"];
}
