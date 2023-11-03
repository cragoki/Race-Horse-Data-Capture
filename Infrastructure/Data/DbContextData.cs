using Core.Entities;
using Core.Interfaces.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Data
{
    public class DbContextData : DbContext, IDbContextData
    {
        public DbContextData(DbContextOptions options)
            : base(options)
        {

        }

        public void DetachAllEntities()
        {
            var changedEntriesCopy = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }

        // Tables
        public DbSet<CourseEntity> tb_course { get; set; }
        public DbSet<EventEntity> tb_event { get; set; }
        public DbSet<BatchEntity> tb_batch { get; set; }
        public DbSet<RaceEntity> tb_race { get; set; }
        public DbSet<HorseEntity> tb_horse { get; set; }
        public DbSet<HorseArchiveEntity> tb_archive_horse { get; set; }
        public DbSet<RaceHorseEntity> tb_race_horse { get; set; }
        public DbSet<TrainerEntity> tb_trainer { get; set; }
        public DbSet<JockeyEntity> tb_jockey { get; set; }
        public DbSet<BacklogDateEntity> tb_backlog_date { get; set; }
        public DbSet<JobEntity> tb_job { get; set; }
        public DbSet<AlgorithmEntity> tb_algorithm { get; set; }
        public DbSet<VariableEntity> tb_variable { get; set; }
        public DbSet<AlgorithmVariableEntity> tb_algorithm_variable { get; set; }
        public DbSet<AgeType> tb_age_type { get; set; }
        public DbSet<DistanceType> tb_distance_type { get; set; }
        public DbSet<GoingType> tb_going_type { get; set; }
        public DbSet<MeetingType> tb_meeting_type { get; set; }
        public DbSet<StallsType> tb_stalls_type { get; set; }
        public DbSet<SurfaceType> tb_surface_type { get; set; }
        public DbSet<WeatherType> tb_weather_type { get; set; }
        public DbSet<AlgorithmSettingsEntity> tb_algorithm_settings { get; set; }
        public DbSet<AlgorithmPredictionEntity> tb_algorithm_prediction { get; set; }
        public DbSet<AlgorithmTrackerEntity> tb_algorithm_tracker { get; set; }
        public DbSet<FailedResultEntity> tb_failed_result { get; set; }
        public DbSet<FailedRaceEntity> tb_failed_race { get; set; }
        public DbSet<AlgorithmSettingsArchiveEntity> tb_algorithm_settings_archive { get; set; }
        public DbSet<AlgorithmVariableSequenceEntity> tb_algorithm_variable_sequence { get; set; }

    }
}
