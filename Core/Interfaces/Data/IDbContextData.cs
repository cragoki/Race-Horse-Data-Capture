using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Interfaces.Data
{
    public interface IDbContextData
    {
        // Tables
        DbSet<CourseEntity> tb_course { get; set; }
        DbSet<EventEntity> tb_event { get; set; }
        DbSet<BatchEntity> tb_batch { get; set; }
        DbSet<RaceEntity> tb_race { get; set; }
        DbSet<HorseEntity> tb_horse { get; set; }
        DbSet<HorseArchiveEntity> tb_archive_horse { get; set; }
        DbSet<RaceHorseEntity> tb_race_horse { get; set; }
        DbSet<TrainerEntity> tb_trainer { get; set; }
        DbSet<JockeyEntity> tb_jockey { get; set; }
        DbSet<BacklogDateEntity> tb_backlog_date { get; set; }
        DbSet<JobEntity> tb_job { get; set; }
        DbSet<AlgorithmEntity> tb_algorithm { get; set; }
        DbSet<VariableEntity> tb_variable { get; set; }
        DbSet<AlgorithmVariableEntity> tb_algorithm_variable { get; set; }
        DbSet<AgeType> tb_age_type { get; set; }
        DbSet<DistanceType> tb_distance_type { get; set; }
        DbSet<GoingType> tb_going_type { get; set; }
        DbSet<MeetingType> tb_meeting_type { get; set; }
        DbSet<StallsType> tb_stalls_type { get; set; }
        DbSet<SurfaceType> tb_surface_type { get; set; }
        DbSet<WeatherType> tb_weather_type { get; set; }
        DbSet<AlgorithmSettingsEntity> tb_algorithm_settings { get; set; }
        DbSet<AlgorithmPredictionEntity> tb_algorithm_prediction { get; set; }
        void DetachAllEntities();
    }
}
