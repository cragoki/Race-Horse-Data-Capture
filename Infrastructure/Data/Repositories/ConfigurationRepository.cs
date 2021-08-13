using Core.Entities;
using Core.Interfaces.Data.Repositories;

namespace Infrastructure.Data.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly DbContextData _context;

        public ConfigurationRepository(DbContextData context)
        {
            _context = context;
        }

        public void AddBatch(BatchEntity batch)
        {
            _context.tb_batch.Add(batch);
            _context.SaveChanges();
        }

    }
}
