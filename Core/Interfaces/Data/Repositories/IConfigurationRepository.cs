using Core.Entities;
using Core.Enums;
using System;

namespace Core.Interfaces.Data.Repositories
{
    public interface IConfigurationRepository
    {
        void AddBatch(BatchEntity batch);
        BacklogDateEntity GetBacklogDate();
        void UpdateBacklogDate(DateTime date);
        JobEntity GetJobInfo(JobEnum job);
        void UpdateNextExecution(JobEnum job);
    }
}