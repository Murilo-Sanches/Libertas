using Libertas.Source.Configurations;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        new AppServices(builder).Add();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        AppPipeline.Configure(app);

        // Handle the requests mapping to the specific action.
        AppRouter.Handle(app);

        app.Run();
    }
}
