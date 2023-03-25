using Microsoft.Extensions.Logging;
using SpaceWScheduler.Models.Interfaces;
using SpaceWScheduler.Models.Models;
using SpaceWScheduler.Services.Interfaces;

namespace SpaceWScheduler.Services.Services
{
    public class EventGetter : IEventGetter
    {

        private readonly ILogger _logger;
        private readonly IMockDB _mockDB;

        public EventGetter(
            ILogger<Event> logger,
            IMockDB mockDB
        ) 
        {
            _logger = logger;
            _mockDB = mockDB;
        }

        /// <inheritdoc/>
        public Event? GetEvent(int id) =>
            _mockDB.GetEventById(id);

        /// <inheritdoc/>
        public IEnumerable<Event> GetEvents() =>
            _mockDB.GetAllEvents();

        /// <inheritdoc/>
        public IEnumerable<Event> GetEventsForSchedule(Schedule schedule) =>
            _mockDB.GetEventsBySchedule(schedule);
    }
}
