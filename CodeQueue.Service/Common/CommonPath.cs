namespace CodeQueue.Service.Common
{
    public class CommonPath
    {
        public static string GetConfigurationDirectory()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration");

            return path;
        }
    }
}
