using Core.Entities;
using Core.Interfaces.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class DbContextData : DbContext, IDbContextData
    {
        public DbContextData(DbContextOptions options)
            : base(options)
        {

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

    }
}
