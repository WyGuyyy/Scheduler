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
        /// <param name="id">The id of the <see cref="Schedule"/> to be fetched</param>
        /// <returns>A <see cref="Schedule"/> with an id matching <paramref name="id"/>. If no matching id is found, return <see cref="default"/></returns>
        public Schedule? GetScheduleById(int id);

        /// <summary>
        /// Get all schedules from the database for a specific date.
        /// </summary>
        /// <param name="date">The date for which to fetch <see cref="Schedule"/> instances</param>
        /// <returns><see cref="IEnumerable{Schedule}"/> containing all of the schedules for a specific <see cref="DateTime"/> <paramref name="date"/></returns>
        public IEnumerable<Schedule> GetSchedulesByDate(DateTime date);

        /// <summary>
        /// Add a <see cref="Event"/> to the database
        /// </summary>
        /// <param name="Event">The <see cref="Event"/> to be added to the database</param>
        public void AddEvent(Event Event);

        /// <summary>
        /// Update a <see cref="Event"/> in the database
        /// </summary>
        /// <param name="Event">The detached <see cref="Event"/> used to update its attached counterpart</param>
        public void UpdateEvent(Event Event);

        /// <summary>
        /// Delete an <see cref="Event"/> from the database
        /// </summary>
        /// <param name="id">The id of the <see cref="Event"/> to be deleted from the database</param>
        public void DeleteEvent(int id);

        /// <summary>
        /// Get all events from the database.
        /// </summary>
        /// <returns><see cref="IEnumerable{Event}"/> contains all of the events currently in the database</returns>
        public IEnumerable<Event> GetAllEvents();

        /// <summary>
        /// Get an <see cref="Event"/> with an id matching <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="Event"/> to be fetched</param>
        /// <returns>An <see cref="Event"/> with an id matching <paramref name="id"/>. If no matching id is found, return <see cref="default"/></returns>
        public Event? GetEventById(int id);

        /// <summary>
        /// Get all events from the database for a specific <see cref="Schedule"/>.
        /// </summary>
        /// <param name="schedule">The <see cref="Schedule"/> for which to fetch <see cref="Event"/> instances</param>
        /// <returns><see cref="IEnumerable{Event}"/> containing all of the events for a specific <see cref="Schedule"/> <paramref name="schedule"/></returns>
        public IEnumerable<Event> GetEventsBySchedule(Schedule schedule);

    }
}
