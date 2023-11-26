using Libertas.Source.Configurations;
using Microsoft.AspNetCore.Mvc.Razor;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        AddServices(builder.Services);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        ConfigurePipeline(app);

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddRouting(options => options.LowercaseUrls = true);
        services.Configure<RazorViewEngineOptions>(options =>
        {
            options.ViewLocationExpanders.Add(new RazorLocationExpander());
        });
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