using CodeQueue.Domain.Models;
using CodeQueue.Service.Common;
using CodeQueue.Service.Middleware;
using Newtonsoft.Json;

namespace CodeQueue;
public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        builder.ConfigureProject();

        CreateFiles();

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

    private static void CreateFiles()
    {
        var directory = CommonPath.GetConfigurationDirectory();
        var fullpath = CommonPath.GetFullPathConfiguration();

        bool newConfiguration = false;

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            newConfiguration = true;
        }

        if (!File.Exists(fullpath))
        {
            File.Create(fullpath).Close();
            newConfiguration = true;
        }

        if (newConfiguration)
        {
            var configuration = new ConfigurationModel()
            {
                Token = "You Token Here"
            };

            File.WriteAllText(fullpath, JsonConvert.SerializeObject(configuration));
        }
    }
}
