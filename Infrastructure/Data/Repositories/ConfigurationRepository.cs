using Core.Entities;
using Core.Enums;
using Core.Interfaces.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly DbContextData _context;

        public ConfigurationRepository(DbContextData context)
        {
            _context = context;
        }

        public async Task AddBatch(BatchEntity batch)
        {
            _context.tb_batch.Add(batch);
            await _context.SaveChangesAsync();
        }

        public BatchEntity GetMostRecentBatch()
        {
            return _context.tb_batch.AsNoTracking().OrderByDescending(x => x.date).ToList().FirstOrDefault();
        }

        public BatchEntity GetNextBatch(Guid currentBatch)
        {
            var batches = _context.tb_batch.AsNoTracking().OrderByDescending(x => x.date).ToList();
            Int32 index = batches.IndexOf(batches.Where(x => x.batch_id == currentBatch).ToList().FirstOrDefault());
            if (index == 0)
            {
                return new BatchEntity();
            }
            return batches.ElementAt(index - 1) ?? null;
        }

        public BatchEntity GetPreviousBatch(Guid currentBatch)
        {
            var batches = _context.tb_batch.AsNoTracking().OrderByDescending(x => x.date).ToList();
            Int32 index = batches.IndexOf(batches.Where(x => x.batch_id == currentBatch).ToList().FirstOrDefault());
            return batches.ElementAt(index + 1) ?? null;
        }

        public BacklogDateEntity GetBacklogDate()
        {
            return _context.tb_backlog_date.ToList().FirstOrDefault();
        }
        public async Task UpdateBacklogDate(DateTime date)
        {
            var toUpdate = new BacklogDateEntity() { backlog_date = date };
            _context.tb_backlog_date.Update(toUpdate);
            await _context.SaveChangesAsync();
        }

        public JobEntity GetJobInfo(JobEnum job)
        {
            return _context.tb_job.Where(x => x.job_id == (int)job).FirstOrDefault();
        }

        public IEnumerable<AlgorithmSettingsEntity> GetAlgorithmSettings(int algorithmId)
        {
            return _context.tb_algorithm_settings.Where(x => x.algorithm_id == algorithmId);
        }

        public async Task UpdateJob(JobEntity job)
        {
            _context.Update(job);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFailedResult(FailedResultEntity entity)
        {
            _context.tb_failed_result.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFailedRace(FailedRaceEntity entity)
        {
            _context.tb_failed_race.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<FailedResultEntity> GetFailedResults()
        {
            return _context.tb_failed_result;
        }

        public IEnumerable<FailedRaceEntity> GetFailedRaces()
        {
            return _context.tb_failed_race.Include(x => x.Race);
        }

        public FailedRaceEntity GetFailedRace(int race_id)
        {
            return _context.tb_failed_race.Where(x => x.race_id == race_id).FirstOrDefault();
        }
        public FailedResultEntity GetFailedResult(int id)
        {
            return _context.tb_failed_result.Where(x => x.failed_result_id == id).FirstOrDefault();
        }


        public async Task AddFailedResult(FailedResultEntity entity)
        {
            _context.tb_failed_result.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddFailedRace(FailedRaceEntity entity)
        {
            _context.tb_failed_race.Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateFailedRace(FailedRaceEntity entity)
        {
            _context.tb_failed_race.Update(entity);
            await _context.SaveChangesAsync();
        }

    }
}
