namespace PlatformService;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            if (env.IsDevelopment())
            {
                Console.WriteLine("==> Using InMemory Database");
                options.UseInMemoryDatabase("InMemory");
            }
            else
            {
                Console.WriteLine("==> Using Sql Server Database");
                options.UseSqlServer(configuration.PlatformsConnectionString());
            }
        });
        services.AddScoped<IPlatformRepository, PlatformRepository>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
        services.AddSingleton<IMessageBusClient, MessageBusClient>();
    }
}
