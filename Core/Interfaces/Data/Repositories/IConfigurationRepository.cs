using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Data.Repositories
{
    public interface IConfigurationRepository
    {
        Task AddBatch(BatchEntity batch);
        BatchEntity GetMostRecentBatch();
        BatchEntity GetNextBatch(Guid currentBatch);
        BatchEntity GetPreviousBatch(Guid currentBatch);
        BacklogDateEntity GetBacklogDate();
        Task UpdateBacklogDate(DateTime date);
        JobEntity GetJobInfo(JobEnum job);
        IEnumerable<AlgorithmSettingsEntity> GetAlgorithmSettings(int algorithmId);
        Task UpdateNextExecution(JobEnum job);
        Task DeleteFailedResult(FailedResultEntity entity);
        IEnumerable<FailedResultEntity> GetFailedResults();
        Task AddFailedResult(FailedResultEntity entity);
        Task AddFailedRace(FailedRaceEntity entity);
        FailedRaceEntity GetFailedRace(int race_id);
        FailedResultEntity GetFailedResult(int id);

        Task UpdateFailedRace(FailedRaceEntity entity);
        IEnumerable<FailedRaceEntity> GetFailedRaces();
        Task DeleteFailedRace(FailedRaceEntity entity);
    }
}