using Libertas.Source.Core.Entities.Context;
using Libertas.Source.Core.Entities.DAO;
using Libertas.Source.Core.Entities.Repositories;
using Libertas.Source.Core.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

namespace Libertas.Source.Configurations;

public class AppServices(WebApplicationBuilder builder)
{
    public IServiceCollection services = builder.Services;
    public ConfigurationManager configuration = builder.Configuration;

    public void Add()
    {
        services.AddControllersWithViews();
        services.AddRouting(options => options.LowercaseUrls = true);
        services.Configure<RazorViewEngineOptions>(options =>
        {
            options.ViewLocationExpanders.Add(new RazorLocationExpander());
        });

        services.AddTransient<IUserDAO, UserRepository>();

        DataService(services, configuration);
    }

    public static void DataService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
        {
            var connection = configuration.GetConnectionString("MySQL");
            var server = new MySqlServerVersion(new Version(8, 0, 35));

            options.UseMySql(connectionString: connection, serverVersion: server);
        });

        var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development;
        var args = Environment.GetCommandLineArgs();

        if (isDevelopment && args.Any(arg => arg.Equals("SEED", StringComparison.CurrentCultureIgnoreCase)))
        {
            var seedCountArgIndex = Array.FindIndex(args, arg => arg.Equals("SEED", StringComparison.CurrentCultureIgnoreCase));

            if (seedCountArgIndex < args.Length - 1 && int.TryParse(args[seedCountArgIndex + 1], out int seedCount))
            {
                services.AddTransient<Faker>();

                using var serviceProvider = services.BuildServiceProvider();
                var faker = serviceProvider.GetRequiredService<Faker>();

                faker.SeedDummyData(seedCount);

                return;
            }

            Logger.Error("Invalid or missing seed count. Usage: SEED <seedCount>");
        }
    }
}
