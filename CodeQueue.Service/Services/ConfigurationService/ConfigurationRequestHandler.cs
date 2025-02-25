using CodeQueue.Domain.Models;
using CodeQueue.Service.Common;
using MediatR;
using Newtonsoft.Json;

namespace CodeQueue.Service.Services.ConfigurationService
{
    public class ConfigurationRequestHandler : IRequestHandler<ConfigurationRequest, ConfigurationModel>
    {
        private readonly string _configurationFile = "Configuration";

        public async Task<ConfigurationModel> Handle(ConfigurationRequest request, CancellationToken cancellationToken = default)
        {
            var directory = CommonPath.GetConfigurationDirectory();
            var fullpath = Path.Combine(directory, $"{_configurationFile}.json");

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(fullpath))
                File.Create(fullpath).Close();

            var content = File.ReadAllText(fullpath);

            var configuration = JsonConvert.DeserializeObject<ConfigurationModel>(content);

            return configuration;
        }
    }
}
