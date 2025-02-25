using System.IO;

namespace CodeQueue.Service.Common
{
    public class CommonPath
    {
        private static readonly string _configurationFilename = "Configuration.json";
        private static readonly string _configurationQueues = "Queues.json";

        public static string GetConfigurationDirectory()
        {
            var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration");

            return directory;
        }

        public static string GetFullPathConfiguration() => Path.Combine(GetConfigurationDirectory(), $"{_configurationFilename}");

        public static string GetFullPathQueues() => Path.Combine(GetConfigurationDirectory(), $"{_configurationQueues}");
    }
}
