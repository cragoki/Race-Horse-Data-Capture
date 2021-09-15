using Core.Entities;
using Core.Interfaces.Data.Repositories;
using System;
using System.Linq;

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

        public BacklogDateEntity GetBacklogDate()
        {
            return _context.tb_backlog_date.FirstOrDefault();
        }
        public void UpdateBacklogDate(DateTime date)
        {
            var toUpdate = new BacklogDateEntity() { backlog_date = date};
            _context.tb_backlog_date.Update(toUpdate);
            _context.SaveChanges();
        }

    }
}
