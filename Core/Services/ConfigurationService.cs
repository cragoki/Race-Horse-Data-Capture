using Core.Models.Settings;
using Microsoft.Extensions.Configuration;
using Core.Interfaces.Services;
using System;
using Core.Entities;
using Core.Interfaces.Data.Repositories;

namespace Core.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _config;
        private static IConfigurationRepository _repository;

        public ConfigurationService(IConfiguration config, IConfigurationRepository repository)
        {
            _config = config;
            _repository = repository;
        }

        public RacingPostSettings GetRacingPostSettings()
        {
            return new RacingPostSettings()
            {
                BaseUrl = _config.GetValue<string>("RacingPostSettings:BaseUrl")
            };
        }

        public void AddBatch(Guid batchId, string diagnostics) 
        {
            var batchEntity = new BatchEntity()
            {
                batch_id = batchId,
                diagnostics = diagnostics,
                date = DateTime.Now
            };

            _repository.AddBatch(batchEntity);
        }
    }
}
