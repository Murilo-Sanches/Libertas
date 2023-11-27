using Libertas.Source.Configurations;
using Libertas.Source.Core.Entities.Context;
using Libertas.Source.Core.Entities.DAO;
using Libertas.Source.Core.Entities.Repositories;
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
        ConfigurePipeline(app);

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

        services.AddDbContext<DataContext>(options =>
        {
            var connection = configuration.GetConnectionString("MySQL");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 35));

            options.UseMySql(connectionString: connection, serverVersion: serverVersion);
        });

        services.AddTransient<IUserDAO, UserRepository>();
    }

    private static void ConfigurePipeline(WebApplication app)
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
