using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpaceWScheduler.Models.Context;
using SpaceWScheduler.Models.Interfaces;
using SpaceWScheduler.Models.Models;
using SpaceWScheduler.Services.Interfaces;

namespace SpaceWScheduler.Services.Services
{
    public class EventGetter : IEventGetter
    {

        private readonly ILogger _logger;
        private readonly IDbContextFactory<SchedulerContext> _contextFactory;

        public EventGetter(
            ILogger<Event> logger,
            IDbContextFactory<SchedulerContext> contextFactory
        ) 
        {
            _logger = logger;
            _contextFactory = contextFactory;
        }

        /// <inheritdoc/>
        public async Task<Event?> GetEvent(int id) 
        {
            Event? result;

            using (var context = _contextFactory.CreateDbContext()) 
            {
                result = await context.Events
                    .Where(e => e.ID == id)
                    .FirstOrDefaultAsync();
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Event>> GetEvents() 
        {
            IEnumerable<Event> result;

            using (var context = _contextFactory.CreateDbContext())
            {
                result = await context.Events
                    .Include(e => e.Schedule)
                    .ToListAsync();
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Event>> GetEventsForSchedule(Schedule schedule) 
        {
            IEnumerable<Event> result;

            using (var context = _contextFactory.CreateDbContext())
            {
                result = await context.Events
                    .Where(e => e.ScheduleId == schedule.ID)
                    .Include(e => e.Schedule)
                    .ToListAsync();
            }

            return result;
        }
    }
}
