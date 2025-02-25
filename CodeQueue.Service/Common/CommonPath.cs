using System.IO;

namespace CodeQueue.Service.Common
{
    public class CommonPath
    {
        private static readonly string _filename = "Configuration";
        private static readonly string _extension = "json";

        public static string GetConfigurationDirectory()
        {
            var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration");

            return directory;
        }

        public static string GetFullPathConfiguration() => Path.Combine(GetConfigurationDirectory(), $"{_filename}.{_extension}");
    }
}
