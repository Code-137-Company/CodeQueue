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
            var fullpath = CommonPath.GetFullPathConfiguration();

            var content = File.ReadAllText(fullpath);

            var configuration = JsonConvert.DeserializeObject<ConfigurationModel>(content);

            return configuration;
        }
    }
}
