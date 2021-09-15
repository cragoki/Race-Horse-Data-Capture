using Core.Entities;
using System;

namespace Core.Interfaces.Data.Repositories
{
    public interface IConfigurationRepository
    {
        void AddBatch(BatchEntity batch);
        BacklogDateEntity GetBacklogDate();
        void UpdateBacklogDate(DateTime date);
    }
}