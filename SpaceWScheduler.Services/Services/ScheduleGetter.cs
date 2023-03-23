using SpaceWScheduler.Models.Models;
using SpaceWScheduler.Services.Interfaces;

namespace SpaceWScheduler.Services.Services
{
    public class ScheduleGetter : IScheduleGetter
    {

        private readonly IMockDB _mockDB;

        public ScheduleGetter(IMockDB mockDB)
        {
            _mockDB = mockDB;
        }

        /// <inheritdoc/>
        public Schedule? GetSchedule(int id) =>
            _mockDB.GetScheduleById(id);

        /// <inheritdoc/>
        public IEnumerable<Schedule> GetSchedules() =>
            _mockDB.GetAllSchedules();

        /// <inheritdoc/>
        public IEnumerable<Schedule> GetSchedulesByDate(DateTime date) =>
            _mockDB.GetSchedulesByDate(date);
    }
}
