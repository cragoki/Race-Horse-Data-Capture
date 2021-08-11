using Core.Models.Settings;
using Microsoft.Extensions.Configuration;
using Core.Interfaces.Services;

namespace Core.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _config;

        public ConfigurationService(IConfiguration config)
        {
            _config = config;
        }

        public RacingPostSettings GetRacingPostSettings()
        {
            return new RacingPostSettings()
            {
                BaseUrl = _config.GetValue<string>("RacingPostSettings:BaseUrl")
            };
        }
    }
}
