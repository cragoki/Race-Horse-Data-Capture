using Core.Entities;
using Core.Enums;
using Core.Interfaces.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public BatchEntity GetMostRecentBatch()
        {
            return _context.tb_batch.AsNoTracking().OrderByDescending(x => x.date).FirstOrDefault();
        }

        public BatchEntity GetNextBatch(Guid currentBatch)
        {
            var batches = _context.tb_batch.AsNoTracking().OrderByDescending(x => x.date).ToList();
            Int32 index = batches.IndexOf(batches.Where(x => x.batch_id == currentBatch).FirstOrDefault());
            if (index == 0)
            {
                return new BatchEntity();
            }
            return batches.ElementAt(index - 1) ?? null;
        }

        public BatchEntity GetPreviousBatch(Guid currentBatch)
        {
            var batches = _context.tb_batch.AsNoTracking().OrderByDescending(x => x.date).ToList();
            Int32 index = batches.IndexOf(batches.Where(x => x.batch_id == currentBatch).FirstOrDefault());
            return batches.ElementAt(index + 1) ?? null;
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

        public List<AlgorithmSettingsEntity> GetAlgorithmSettings(int algorithmId)
        {
            return _context.tb_algorithm_settings.Where(x => x.algorithm_id == algorithmId).ToList();
        }

        public void UpdateNextExecution(JobEnum job)
        {
            var jobDb = _context.tb_job.Where(x => x.job_id == (int)job).FirstOrDefault();
            var tomorrow = DateTime.Now.AddDays(1);
            switch (job) 
            {
                case JobEnum.rhdcautomation:
                    jobDb.next_execution = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 07, 00, 00);
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
