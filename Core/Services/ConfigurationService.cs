using Core.Models.Settings;
using Microsoft.Extensions.Configuration;
using Core.Interfaces.Services;
using System;
using Core.Entities;
using Core.Interfaces.Data.Repositories;
using System.Threading.Tasks;

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
                BaseUrl = _config.GetValue<string>("RacingPostSettings:BaseUrl"),
                BacklogUrl = _config.GetValue<string>("RacingPostSettings:BacklogUrl")
            };
        }

        public bool SavePermitted()
        {
            return _config.GetValue<bool>("AppSettings:SaveData");
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

        public DateTime GetLastBackfillDate()
        {
            var result = _repository.GetBacklogDate();

            return result.backlog_date;
        }

        public async Task UpdateBackfillDate(DateTime newDate) 
        {
            _repository.UpdateBacklogDate(newDate);
        }
    }
}
