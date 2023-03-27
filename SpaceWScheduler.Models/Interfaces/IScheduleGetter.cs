using SpaceWScheduler.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWScheduler.Services.Interfaces
{
    public interface IScheduleGetter
    {
        /// <summary>
        /// Get all <see cref="Schedule"/> objects that exist in the database
        /// </summary>
        /// <returns><see cref="IEnumerable{Schedule}"/> representing all <see cref="Schedule"/> objects in the database</returns>
        public Task<IEnumerable<Schedule>> GetSchedules();

        /// <summary>
        /// Get a <see cref="Schedule"/> object with id matching <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id of the <see cref="Schedule"/> to search on</param>
        /// <returns>A <see cref="Schedule"/> with id matching <paramref name="id"/></returns>
        public Task<Schedule?> GetSchedule(int id);

        /// <summary>
        /// Get a <see cref="Schedule"/> object for a particular <see cref="DateTime"/>.
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/> of the <see cref="Schedule"/> to search on</param>
        /// <returns>A <see cref="Schedule"/> on a particular <see cref="DateTime"/> matching <paramref name="date"/></returns>
        public Task<IEnumerable<Schedule>> GetSchedulesByDate(DateTime date);
    }
}
