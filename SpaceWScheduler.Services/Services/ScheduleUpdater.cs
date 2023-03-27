using Microsoft.EntityFrameworkCore;
using SpaceWScheduler.Models.Context;
using SpaceWScheduler.Models.Models;
using SpaceWScheduler.Services.Interfaces;

namespace SpaceWScheduler.Services.Services
{
    public class ScheduleUpdater : IScheduleUpdater
    {

        private readonly IDbContextFactory<SchedulerContext> _contextFactory;

        public ScheduleUpdater(IDbContextFactory<SchedulerContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        /// <inheritdoc/>
        public void AddSchedule(Schedule schedule)
        {
            try
            {
                _mockDB.AddSchedule(schedule);
            }
            catch (Exception exc) {
                Console.WriteLine($"Error writing to database: {exc.Message}");
                throw;
            }
        }

        /// <inheritdoc/>
        public void DeleteSchedule(int id)
        {
            try 
            {
                _mockDB.DeleteSchedule(id);
            } 
            catch (Exception exc) {
                Console.WriteLine($"Error writing to database: {exc.Message}");
                throw;
            }
        }

        /// <inheritdoc/>
        public void UpdateSchedule(Schedule schedule)
        {
            try
            {
                _mockDB.UpdateSchedule(schedule);
            }
            catch (Exception exc) {
                Console.WriteLine($"Error writing to database: {exc.Message}");
                throw;
            } 
        }
    }
}
