using Microsoft.Extensions.Logging;
using SpaceWScheduler.Models.Interfaces;
using SpaceWScheduler.Models.Models;
using SpaceWScheduler.Services.Interfaces;

namespace SpaceWScheduler.Services.Services
{
    public class EventUpdater : IEventUpdater
    {

        private readonly ILogger<Event> _logger;
        private readonly IMockDB _mockDB;

        public EventUpdater(
            ILogger<Event> logger,
            IMockDB mockDB
        ) 
        { 
            _logger = logger;
            _mockDB = mockDB;
        }

        /// <inheritdoc/>
        public void AddEvent(Event Event)
        {
            try
            {
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
                _mockDB.UpdateEvent(Event);
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Error writing to database: {exc.Message}");
                throw;
            }
        }
    }
}
