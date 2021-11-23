using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;

namespace Core.Interfaces.Data.Repositories
{
    public interface IConfigurationRepository
    {
        void AddBatch(BatchEntity batch);
        BacklogDateEntity GetBacklogDate();
        void UpdateBacklogDate(DateTime date);
        JobEntity GetJobInfo(JobEnum job);
        List<AlgorithmSettingsEntity> GetAlgorithmSettings(int algorithmId);
        void UpdateNextExecution(JobEnum job);
    }
}