using SpaceWScheduler.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWScheduler.Models.Interfaces
{
    public interface IEventGetter
    {
        /// <summary>
        /// Get all <see cref="Event"/> objects that exist in the database
        /// </summary>
        /// <returns><see cref="IEnumerable{Event}"/> representing all <see cref="Event"/> objects in the database</returns>
        public IEnumerable<Event> GetEvents();

        /// <summary>
        /// Get an <see cref="Event"/> object with id matching <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id of the <see cref="Event"/> to search on</param>
        /// <returns>An <see cref="Event"/> with id matching <paramref name="id"/></returns>
        public Event? GetEvent(int id);

        /// <summary>
        /// Get an <see cref="Event"/> object for a particular <see cref="Schedule"/>.
        /// </summary>
        /// <param name="schedule">The <see cref="Schedule"/> of the <see cref="Event"/> to search on</param>
        /// <returns><see cref="IEnumerable{Event}"/> containing the events for the supplied <see cref="Schedule"/></returns>
        public IEnumerable<Event> GetEventsForSchedule(Schedule schedule);
    }
}
