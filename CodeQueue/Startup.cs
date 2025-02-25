using CodeQueue.Domain.Models;
using CodeQueue.Service.Common;
using CodeQueue.Service.Services.QueueConsumerService;
using Newtonsoft.Json;

namespace CodeQueue
{
    public partial class Program
    {
        public static void StartProject(this IServiceProvider serviceProvider)
        {
            CreateFiles();

            serviceProvider.StartQueues();
        }

        private static void CreateFiles()
        {
            var directory = CommonPath.GetConfigurationDirectory();
            var fullpath = CommonPath.GetFullPathConfiguration();

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(fullpath))
            {
                File.Create(fullpath).Close();

                var configuration = new ConfigurationModel()
                {
                    Token = "You Token Here"
                };

                File.WriteAllText(fullpath, JsonConvert.SerializeObject(configuration));
            }

            var pathQueues = CommonPath.GetFullPathQueues();

            if (!File.Exists(pathQueues))
            {
                File.Create(pathQueues).Close();

                var queues = new List<QueueModel>();

                File.WriteAllText(pathQueues, JsonConvert.SerializeObject(queues));
            }
        }

        private static void StartQueues(this IServiceProvider serviceProvider)
        {
            var queueConsumer = serviceProvider.GetRequiredService<QueueConsumerService>();

            Thread queueThread = new Thread(queueConsumer.Start);

            queueThread.Start();
        }
    }
}
