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
        /// Query <see cref="Schedule"/> based on some <see cref="PropertyInfo"/> parameter and value
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Schedule> QuerySchedules<ValueType>(PropertyInfo prop, ValueType value);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Schedule> GetAllSchedules();

    }
}
