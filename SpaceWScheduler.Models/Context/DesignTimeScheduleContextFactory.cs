using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SpaceWScheduler.Models.Context
{
    public class DesignTimeScheduleContextFactory : IDesignTimeDbContextFactory<SchedulerContext>
    {
        public SchedulerContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SchedulerContext>();
            optionsBuilder.UseSqlite("Data Source=Scheduler.db");

            return new SchedulerContext(optionsBuilder.Options);
        }
    }
}
