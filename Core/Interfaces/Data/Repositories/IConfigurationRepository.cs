using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;

namespace Core.Interfaces.Data.Repositories
{
    public interface IConfigurationRepository
    {
        void AddBatch(BatchEntity batch);
        BatchEntity GetMostRecentBatch();
        BatchEntity GetNextBatch(Guid currentBatch);
        BatchEntity GetPreviousBatch(Guid currentBatch);
        BacklogDateEntity GetBacklogDate();
        void UpdateBacklogDate(DateTime date);
        JobEntity GetJobInfo(JobEnum job);
        IEnumerable<AlgorithmSettingsEntity> GetAlgorithmSettings(int algorithmId);
        void UpdateNextExecution(JobEnum job);
    }
}