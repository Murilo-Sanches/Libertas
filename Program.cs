using Libertas.Source.Configurations;
using Libertas.Source.Core.Entities.Context;
using Libertas.Source.Core.Entities.DAO;
using Libertas.Source.Core.Entities.Repositories;
using Libertas.Source.Core.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        AddServices(builder);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        CreatePipeline(app);

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }

    private static void AddServices(WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

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

        if (isDevelopment && Environment.GetCommandLineArgs().Any(arg => arg.Equals("SEED", StringComparison.CurrentCultureIgnoreCase)))
        {
            var seedCountArgIndex = Array.FindIndex(Environment.GetCommandLineArgs(), arg => arg.Equals("SEED", StringComparison.CurrentCultureIgnoreCase));

            if (seedCountArgIndex < Environment.GetCommandLineArgs().Length - 1 && int.TryParse(Environment.GetCommandLineArgs()[seedCountArgIndex + 1], out int seedCount))
            {
                services.AddTransient<Faker>();

                using var serviceProvider = services.BuildServiceProvider();
                var faker = serviceProvider.GetRequiredService<Faker>();

                faker.SeedDummyData(seedCount);

                return;
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"{Environment.NewLine}Invalid or missing seed count. Usage: SEED <seedCount>{Environment.NewLine}");
            Console.ResetColor();
        }
    }

    private static void CreatePipeline(WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
    }
}
