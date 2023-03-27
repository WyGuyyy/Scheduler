using SpaceWScheduler.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWScheduler.Services.Interfaces
{
    public interface IScheduleUpdater
    {
        /// <summary>
        /// Create and add a <see cref="Schedule"/> to the database
        /// </summary>
        /// <param name="schedule">The <see cref="Schedule"/> object to be added to the database</param>
        public Task AddSchedule(Schedule schedule);

        /// <summary>
        /// Update a <see cref="Schedule"/> in the database
        /// </summary>
        /// <param name="schedule">the detached <see cref="Schedule"/> used to update a record in the database</param>
        public Task UpdateSchedule(Schedule schedule);

        /// <summary>
        /// Delete a <see cref="Schedule"/> from the database.
        /// </summary>
        /// <param name="id">The id of the <see cref="Schedule"/> to be deleted</param>
        public Task DeleteSchedule(int id);
    }
}
