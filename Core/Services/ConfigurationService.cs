using Core.Models.Settings;
using Microsoft.Extensions.Configuration;
using Core.Interfaces.Services;
using System;
using Core.Entities;
using Core.Interfaces.Data.Repositories;
using System.Threading.Tasks;
using Core.Enums;
using System.Collections.Generic;
using System.Linq;

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

        public MailSettings GetMailSettings() 
        {
            return new MailSettings()
            {
                Mail = _config.GetValue<string>("MailSettings:Mail"),
                DisplayName = _config.GetValue<string>("MailSettings:DisplayName"),
                Password = _config.GetValue<string>("MailSettings:Password"),
                Host = _config.GetValue<string>("MailSettings:Host"),
                Port = _config.GetValue<int>("MailSettings:Port")
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

        public async Task<bool> UpdateJob(JobEnum job) 
        {
            try
            {
                _repository.UpdateNextExecution(job);

                return true;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<JobEntity> GetJobInfo(JobEnum job)
        {
            return _repository.GetJobInfo(job);
        }

        public List<AlgorithmSettingsEntity> GetAlgorithmSettings(int algorithmId) 
        {
            return _repository.GetAlgorithmSettings(algorithmId).ToList();
        }
    }
}
