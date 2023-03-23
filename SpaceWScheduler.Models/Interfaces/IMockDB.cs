using SpaceWScheduler.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWScheduler.Services.Interfaces
{
    public interface IMockDB
    {
        /// <summary>
        /// Add a <see cref="Schedule"/> to the database
        /// </summary>
        /// <param name="schedule">The <see cref="Schedule"/> to be added to the database</param>
        public void AddSchedule(Schedule schedule);

        /// <summary>
        /// Update a <see cref="Schedule"/> in the database
        /// </summary>
        /// <param name="schedule">The detached <see cref="Schedule"/> used to update its attached counterpart</param>
        public void UpdateSchedule(Schedule schedule);

        /// <summary>
        /// Delete a <see cref="Schedule"/> from the database
        /// </summary>
        /// <param name="id">The id of the <see cref="Schedule"/> to be deleted from the database</param>
        public void DeleteSchedule(int id);

        /// <summary>
        /// Get all schedules from the database.
        /// </summary>
        /// <returns><see cref="IEnumerable{Schedule}"/> contains all of the schedules currently in the database</returns>
        public IEnumerable<Schedule> GetAllSchedules();

        /// <summary>
        /// Get a <see cref="Schedule"/> with an id matching <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id of the schedule to be fetched</param>
        /// <returns>A <see cref="Schedule"/> with an id matching <paramref name="id"/>. If no matching id is found, return <see cref="default"/></returns>
        public Schedule? GetScheduleById(int id);

        /// <summary>
        /// Get all schedules from the database for a specific date.
        /// </summary>
        /// <param name="date">The date for which to fetch <see cref="Schedule"/> instances</param>
        /// <returns><see cref="IEnumerable{Schedule}"/> containing all of the schedules for a specific <see cref="DateTime"/> <paramref name="date"/></returns>
        public IEnumerable<Schedule> GetSchedulesByDate(DateTime date);

    }
}
