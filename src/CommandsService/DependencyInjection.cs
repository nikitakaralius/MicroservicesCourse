namespace CommandsService;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("InMemory"));
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<ICommandRepository, CommandRepository>();
        services.AddSingleton<IEventProcessor, EventProcessorRouter>();
        services.AddHostedService<MessageBusSubscriber>();
        return services;
    }
}
