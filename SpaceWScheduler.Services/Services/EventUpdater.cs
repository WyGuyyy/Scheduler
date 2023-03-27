using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using SpaceWScheduler.Models.Context;
using SpaceWScheduler.Models.Interfaces;
using SpaceWScheduler.Models.Models;
using SpaceWScheduler.Services.Interfaces;

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
        public void AddEvent(Event Event)
        {
            try
            {
                if (!scheduleExists(Event.ScheduleId)) {
                    throw new Exception("Cannot assign Event to Schedule that does not yet exist.");
                }

                _mockDB.AddEvent(Event);
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Error writing to database: {exc.Message}");
                throw;
            }
        }

        /// <inheritdoc/>
        public void DeleteEvent(int id)
        {
            try
            {
                _mockDB.DeleteEvent(id);
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Error writing to database: {exc.Message}");
                throw;
            }
        }

        /// <inheritdoc/>
        public void UpdateEvent(Event Event)
        {
            try
            {
                if (!scheduleExists(Event.ScheduleId))
                {
                    throw new Exception("Cannot assign Event to Schedule that does not yet exist.");
                }

                _mockDB.UpdateEvent(Event);
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Error writing to database: {exc.Message}");
                throw;
            }
        }

        #endregion Public Methods

        #region Private Methods

        private bool scheduleExists(int scheduleId) =>
            _scheduleGetter.GetSchedule(scheduleId) != default;

        #endregion Private Methods
    }
}
