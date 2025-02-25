using CodeQueue.Service.Middleware;

namespace CodeQueue;
public static partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        builder.ConfigureMediatR();

        builder.Services.ConfigureDatabase();
        builder.Services.ConfigureServices();

        var app = builder.Build();

        app.Services.StartProject();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseMiddleware<ValidateRequestToken>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
