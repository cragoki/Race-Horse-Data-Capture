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

    }
}
