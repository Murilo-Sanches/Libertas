namespace Libertas.Source.Configurations;

class AppRouter
{
    internal static void Handle(WebApplication app)
    {
        app.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");
    }
}
