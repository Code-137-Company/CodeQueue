using Code137.JsonDb.Models;
using Code137.JsonDb;
using CodeQueue.Domain.Entities;
using CodeQueue.Service.Services.QueueConsumerService;

namespace CodeQueue
{
    public partial class Program
    {
        private static void ConfigureMediatR(this WebApplicationBuilder builder)
        {
            var assemblyServices = AppDomain.CurrentDomain.Load("CodeQueue.Service");
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assemblyServices));
        }

        private static void ConfigureDatabase(this IServiceCollection service)
        {
            service.AddSingleton(_ =>
            {
                var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Databases");

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var jsonDb = new JsonDb(new DatabaseOptions("CodeQueue", path: directory));

                jsonDb.AddEntity<Queue>();

                return jsonDb;
            });
        }

        private static void ConfigureServices(this IServiceCollection service)
        {
            service.AddSingleton<QueueConsumerService>();
        }
    }
}
