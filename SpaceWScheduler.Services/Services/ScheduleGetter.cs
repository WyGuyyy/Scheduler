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
        public Schedule GetSchedule(int id) =>
            _mockDB.MOCK_SCHEDULE_DB.ContainsKey(id) ?
            _mockDB.MOCK_SCHEDULE_DB[id] : default;

        /// <inheritdoc/>
        public IEnumerable<Schedule> GetSchedules() =>
            _mockDB.MOCK_SCHEDULE_DB.Values;

        /// <inheritdoc/>
        public IEnumerable<Schedule> GetScheduleByDate(DateTime date) {
            IEnumerable<Schedule> schedules = _mockDB.MOCK_SCHEDULE_DB.Values;
            IList<Schedule> result = new List<Schedule>();

            foreach (Schedule s in schedules) {
                if (s.StartTime.Date.CompareTo(date.Date) == 0) {
                    result.Add(s);
                }
            }

            return result;
        }
    }
}
