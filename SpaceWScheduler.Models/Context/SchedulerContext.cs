using Microsoft.EntityFrameworkCore;
using SpaceWScheduler.Models.Configuration;
using SpaceWScheduler.Models.Models;

namespace SpaceWScheduler.Models.Context
{
    public class SchedulerContext : DbContext
    {
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Event> Events { get; set; }

        public SchedulerContext(DbContextOptions<SchedulerContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ScheduleConfiguration().Configure(modelBuilder.Entity<Schedule>());
            new EventConfiguration().Configure(modelBuilder.Entity<Event>());
        }
    }
}
