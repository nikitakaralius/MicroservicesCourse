namespace CommandsService.Extensions;

public static class ConfigurationExtensions
{
    public static string RabbitMQHost(this IConfiguration configuration) =>
        configuration["RabbitMQHost"];

    public static int RabbitMQPort(this IConfiguration configuration) =>
        int.Parse(configuration["RabbitMQPort"]);

    public static string RabbitMQExchange(this IConfiguration configuration) =>
        configuration["RabbitMQExchange"];
}
