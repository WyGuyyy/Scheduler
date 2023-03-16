using SpaceWScheduler.Models.Models;
using SpaceWScheduler.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWScheduler.Services.Services
{
    public class ScheduleUpdater : IScheduleUpdater
    {

        private readonly IMockDB _mockDB;

        public ScheduleUpdater(IMockDB mockDB) 
        {
            _mockDB = mockDB;
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
