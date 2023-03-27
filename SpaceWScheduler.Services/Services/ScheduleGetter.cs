using Microsoft.EntityFrameworkCore;
using SpaceWScheduler.Models.Context;
using SpaceWScheduler.Models.Interfaces;
using SpaceWScheduler.Models.Models;
using SpaceWScheduler.Services.Interfaces;

namespace SpaceWScheduler.Services.Services
{
    public class ScheduleGetter : IScheduleGetter
    {

        private readonly IDbContextFactory<SchedulerContext> _contextFactory;
        private readonly IEventGetter _eventGetter;

        public ScheduleGetter(
            IDbContextFactory<SchedulerContext> contextFactory,
            IEventGetter eventGetter
        )
        {
            _contextFactory = contextFactory;
            _eventGetter = eventGetter;
        }

        /// <inheritdoc/>
        public async Task<Schedule?> GetSchedule(int id)
        {
            Schedule? schedule = default;

            using (var context = _contextFactory.CreateDbContext())
            {
                schedule = await context.Schedules
                    .Where(s => s.ID == id)
                    .Include(s => s.Events)
                    .FirstOrDefaultAsync();
            }

            return schedule;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Schedule>> GetSchedules()
        {
            IEnumerable<Schedule> schedules;

            using (var context = _contextFactory.CreateDbContext())
            {
                schedules = await context.Schedules
                    .Include(s => s.Events)
                    .ToListAsync();
            }

            return schedules;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Schedule>> GetSchedulesByDate(DateTime date) 
        {
            IEnumerable<Schedule> result;

            using (var context = _contextFactory.CreateDbContext()) 
            {
                result = await context.Schedules
                    .Where(s => s.StartTime.HasValue && s.StartTime.Value.Date.CompareTo(date.Date) == 0)
                    .Include(s => s.Events)
                    .ToListAsync();
            }

            return result;
        }
    }
}
