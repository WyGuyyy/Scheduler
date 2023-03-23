using SpaceWScheduler.Models.Helpers;
using SpaceWScheduler.Models.Models;
using SpaceWScheduler.Services.Interfaces;

namespace SpaceWScheduler.Services.Services
{
    public class MockDB : IMockDB
    {
        // Our nieve implementation for an in memory database, which is initialized in the constructor.
        // As MockDB is a singleton service, this initialization will only happen the first time a user
        // request requires the MockDB
        private IDictionary<int, Schedule> MOCK_SCHEDULE_DB;
        private static int idCounter = 1;

        public MockDB() {
            MOCK_SCHEDULE_DB = new Dictionary<int, Schedule>();
            MOCK_SCHEDULE_DB.Add(1, new Schedule(idCounter, DateTime.Now.Date, DateTime.Now.Date.AddHours(8), "Example Schedule"));
            idCounter++;
        }

        #region Public Methods
        /// <inheritdoc/>
        public void AddSchedule(Schedule schedule)
        {
            // Our idempotency key is the Date (excluding the time component) of our Schedule + the Name
            // of the schedule. The ID is just a unique identifer for a row. So if this idempotency key is
            // violated with this new Schedule addition, throw an exception.
            foreach (Schedule s in MOCK_SCHEDULE_DB.Values) 
            {
                if (s.StartTime.Date.CompareTo(schedule.StartTime.Date) == 0 &&
                    s.Name.Equals(schedule.Name)) {
                    throw new Exception("Identical schedule already exists.");
                }    
            }

            MOCK_SCHEDULE_DB.Add(idCounter, schedule);
            incrementCounter();
        }

        /// <inheritdoc/>
        public void UpdateSchedule(Schedule schedule)
        {
            Schedule? attachedSchedule = default;

            foreach (Schedule s in MOCK_SCHEDULE_DB.Values) 
            {
                if (s.StartTime.Date.CompareTo(schedule.StartTime.Date) == 0 &&
                    s.Name.Equals(schedule.Name))
                {
                    attachedSchedule = s;
                    break;
                }
            }

            if (attachedSchedule == default) {
                AddSchedule(schedule);
                return;
            }

            schedule.FillEmptyFields(attachedSchedule);
            MOCK_SCHEDULE_DB.Add(idCounter, schedule);
            incrementCounter();
        }

        /// <inheritdoc/>
        public void DeleteSchedule(int id)
        {
            if (!MOCK_SCHEDULE_DB.ContainsKey(id)) {
                throw new Exception($"Schedule with id {id} does not exist. Delete action canceled.");
            }

            MOCK_SCHEDULE_DB.Remove(id);
        }

        /// <inheritdoc/>
        public IEnumerable<Schedule> GetAllSchedules() => MOCK_SCHEDULE_DB.Values;

        /// <inheritdoc/>
        public Schedule? GetScheduleById(int id) =>
            MOCK_SCHEDULE_DB.ContainsKey(id) ?
            MOCK_SCHEDULE_DB[id] :
            default;

        /// <inheritdoc/>
        public IEnumerable<Schedule> GetSchedulesByDate(DateTime date) {

            IList<Schedule> result = new List<Schedule>();

            foreach (Schedule s in MOCK_SCHEDULE_DB.Values)
            {
                if (s.StartTime.Date.CompareTo(date.Date) == 0)
                {
                    result.Add(s);
                }
            }

            return result;
        }
        #endregion Public Methods

        #region Private Methods
        private void incrementCounter() => idCounter++;
        #endregion Private Methods
    }
}

