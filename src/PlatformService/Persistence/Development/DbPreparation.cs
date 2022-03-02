namespace PlatformService.Persistence.Development;

public static class DbPreparation
{
    public static void PrepareDbPopulation(this IApplicationBuilder app)
    {
        using IServiceScope serviceScope = app.ApplicationServices.CreateScope();
        ApplicationDbContext? context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
        context?.SeedData();
    }

    private static void SeedData(this ApplicationDbContext context)
    {
        if (context.Platforms.Any())
        {
            Console.WriteLine("==> We already have data");
            return;
        }
        Console.WriteLine("==> Seeding Data...");
        context.Platforms.AddRange(new Platform[]
        {
            new(1, ".NET", "Microsoft", "Free"),
            new(2, "SQL Server Express", "Microsoft", "Free"),
            new(3, "Kubernetes", "Cloud Native Computing Foundation", "Free")
        });
        context.SaveChanges();
    }
}
