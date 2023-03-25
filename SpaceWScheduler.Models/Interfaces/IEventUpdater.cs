using SpaceWScheduler.Models.Models;

namespace SpaceWScheduler.Models.Interfaces
{
    public interface IEventUpdater
    {
        /// <summary>
        /// Create and add a <see cref="Event"/> to the database
        /// </summary>
        /// <param name="Event">The <see cref="Event"/> object to be added to the database</param>
        public void AddEvent(Event Event);

        /// <summary>
        /// Update a <see cref="Event"/> in the database
        /// </summary>
        /// <param name="event">the detached <see cref="Event"/> used to update a record in the database</param>
        public void UpdateEvent(Event Event);

        /// <summary>
        /// Delete a <see cref="Event"/> from the database.
        /// </summary>
        /// <param name="id">The id of the <see cref="Event"/> to be deleted</param>
        public void DeleteEvent(int id);
    }
}
