using Code137.JsonDb;
using Code137.JsonDb.Models;
using CodeQueue.Service.Middleware;
using CodeQueue.Service.Services.Messages.PublishMessageService;

namespace CodeQueue;
public static class Program
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

    private static void ConfigureMediatR(this WebApplicationBuilder builder)
    {
        var assemblyServices = AppDomain.CurrentDomain.Load("CodeQueue.Service");
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assemblyServices));
    }

    private static void ConfigureDatabase(this IServiceCollection service)
    {
        service.AddSingleton(_ =>
        {
            var jsonDb = new JsonDb(new DatabaseOptions("CodeQueue", path: AppDomain.CurrentDomain.BaseDirectory));

            //jsonDb.AddEntity<>();

            return jsonDb;
        });
    }

    private static void ConfigureServices(this IServiceCollection service)
    {
        //service.AddTransient<PublishMessageRequest>();
    }
}
