using Core.Entities;

namespace Core.Interfaces.Data.Repositories
{
    public interface IConfigurationRepository
    {
        void AddBatch(BatchEntity batch);
    }
}