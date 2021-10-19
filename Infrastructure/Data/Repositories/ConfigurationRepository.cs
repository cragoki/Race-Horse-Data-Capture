using Core.Entities;
using Core.Enums;
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

        public JobEntity GetJobInfo(JobEnum job)
        {
            return _context.tb_job.Where(x => x.job_id == (int)job).FirstOrDefault();
        }

        public void UpdateNextExecution(JobEnum job)
        {
            var jobDb = _context.tb_job.Where(x => x.job_id == (int)job).FirstOrDefault();
            var tomorrow = DateTime.Now.AddDays(1);
            switch (job) 
            {
                case JobEnum.rhdcautomation:
                    jobDb.next_execution = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 09, 00, 00);
                    break;
                case JobEnum.rhdcbacklog:
                    break;
                case JobEnum.rhdcresultretriever:
                    jobDb.next_execution = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 22, 00, 00);
                    break;
            }
            jobDb.last_execution = DateTime.Now;

            _context.tb_job.Update(jobDb);
            _context.SaveChanges();
        }

    }
}
