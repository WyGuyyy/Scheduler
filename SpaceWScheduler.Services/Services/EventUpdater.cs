using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using SpaceWScheduler.Models.Context;
using SpaceWScheduler.Models.Helpers;
using SpaceWScheduler.Models.Interfaces;
using SpaceWScheduler.Models.Models;
using SpaceWScheduler.Services.Interfaces;
using System.Data;

namespace SpaceWScheduler.Services.Services
{

    public class EventUpdater : IEventUpdater
    {

        private readonly ILogger<Event> _logger;
        private readonly IDbContextFactory<SchedulerContext> _contextFactory;
        private readonly IScheduleGetter _scheduleGetter;

        public EventUpdater(
            ILogger<Event> logger,
            IDbContextFactory<SchedulerContext> contextFactory,
            IScheduleGetter scheduleGetter
        ) 
        { 
            _logger = logger;
            _contextFactory = contextFactory;
            _scheduleGetter = scheduleGetter;
        }

        #region Public Methods

        /// <inheritdoc/>
        public async Task AddEvent(Event Event)
        {
            if (!(await scheduleExists(Event.ScheduleId))) {
                throw new Exception("Cannot assign Event to Schedule that does not yet exist.");
            }

            using (var context = _contextFactory.CreateDbContext()) {
                if (await eventsOverlap(Event, context))
                {
                    throw new Exception("This Event overlaps with another for the same Schedule.");
                }

                context.Add(Event);
                int result = context.SaveChanges();

                if (result < 1) {
                    throw new Exception("Issue writing to the database to create new Event.");
                }
            }
        }

        /// <inheritdoc/>
        public async Task DeleteEvent(int id)
        {
            using (var context = _contextFactory.CreateDbContext()) {
                Event? e = await context.Events
                    .Where(e => e.ID == id)
                    .FirstOrDefaultAsync();

                if (e != default) { 
                    context.Remove(e);
                    int result = context.SaveChanges();

                    if (result < 1) {
                        throw new Exception($"Issue removing Event with ID {e.ID} from the database.");
                    }
                }
            }
        }

        /// <inheritdoc/>
        public async Task UpdateEvent(Event Event)
        {
            if (!(await scheduleExists(Event.ScheduleId)))
            {
                throw new Exception("Cannot assign Event to Schedule that does not yet exist.");
            }

            using (var context = _contextFactory.CreateDbContext()) {

                if (await eventsOverlap(Event, context))
                {
                    throw new Exception("This Event overlaps with another for the same Schedule.");
                }

                Event? attachedEvent = await context.Events
                    .Where(e => e.ID == Event.ID)
                    .FirstOrDefaultAsync();

                if (attachedEvent == default) {
                    await AddEvent(Event);
                    return;
                }

                attachedEvent.ReplacePopulatedFields(Event);
                context.Update(Event);
                int result = context.SaveChanges();

                if (result < 1)
                {
                    throw new Exception($"Issue writing to the database to update Event with ID {Event.ID}.");
                }

            }
        }

        #endregion Public Methods

        #region Private Methods

        private async Task<bool> scheduleExists(int scheduleId) =>
            await _scheduleGetter.GetSchedule(scheduleId) != default;

        private async Task<bool> eventsOverlap(Event Event, SchedulerContext context) 
        {
            Event? overlappingEvent = await context.Events
                .Where(e =>
                    e.ScheduleId == Event.ScheduleId && 
                    e.StartTime.HasValue && e.EndTime.HasValue &&
                    Event.StartTime.HasValue && Event.EndTime.HasValue &&
                    e.StartTime.Value.Date.CompareTo(Event.StartTime.Value.Date) == 0 &&
                    e.StartTime.Value.TimeOfDay.CompareTo(Event.EndTime.Value.TimeOfDay) == -1 &&
                    e.EndTime.Value.Date.CompareTo(Event.EndTime.Value.Date) == 0 &&
                    e.EndTime.Value.TimeOfDay.CompareTo(Event.StartTime.Value.TimeOfDay) == 1
                )
                .FirstOrDefaultAsync();

            return overlappingEvent != null;
        }

        #endregion Private Methods
    }
}
