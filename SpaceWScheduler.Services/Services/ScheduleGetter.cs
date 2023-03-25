using SpaceWScheduler.Models.Interfaces;
using SpaceWScheduler.Models.Models;
using SpaceWScheduler.Services.Interfaces;

namespace SpaceWScheduler.Services.Services
{
    public class ScheduleGetter : IScheduleGetter
    {

        private readonly IMockDB _mockDB;
        private readonly IEventGetter _eventGetter;

        public ScheduleGetter(
            IMockDB mockDB,
            IEventGetter eventGetter
        )
        {
            _mockDB = mockDB;
            _eventGetter = eventGetter;
        }

        /// <inheritdoc/>
        public Schedule? GetSchedule(int id)
        {
            Schedule? schedule = _mockDB.GetScheduleById(id);

            // Populate the events for the schedule
            if (schedule != default) {
                schedule.Events = _eventGetter.GetEventsForSchedule(schedule).ToList();
            }

            return schedule;
        }

        /// <inheritdoc/>
        public IEnumerable<Schedule> GetSchedules()
        {
            IEnumerable<Schedule> schedules = _mockDB.GetAllSchedules();

            // Populate the events for every schedule
            foreach (Schedule s in schedules) {
                s.Events = _eventGetter.GetEventsForSchedule(s).ToList();
            }

            return schedules;
        }

        /// <inheritdoc/>
        public IEnumerable<Schedule> GetSchedulesByDate(DateTime date) =>
            _mockDB.GetSchedulesByDate(date);
    }
}
