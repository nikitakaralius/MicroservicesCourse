namespace PlatformService;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("InMemory"));
        services.AddScoped<IPlatformRepository, PlatformRepository>();
    }
}
