using SpaceWScheduler.Models.Helpers;
using SpaceWScheduler.Models.Models;
using SpaceWScheduler.Services.Interfaces;
using System.Diagnostics.Tracing;

namespace SpaceWScheduler.Services.Services
{
    public class MockDB : IMockDB
    {
        // Our nieve implementation for an in memory database, which is initialized in the constructor.
        // As MockDB is a singleton service, this initialization will only happen the first time a user
        // request requires the MockDB
        private IDictionary<int, Schedule> MOCK_SCHEDULE_DB;
        private IDictionary<int, Event> MOCK_EVENT_DB;
        private static int scheduleIdCounter = 1;
        private static int eventIDCounter = 1;

        public MockDB() {
            MOCK_SCHEDULE_DB = new Dictionary<int, Schedule>
            {
                { 1, new Schedule { ID = 1, StartTime = DateTime.Now.Date, EndTime = DateTime.Now.Date.AddHours(8), Name = "Example Schedule" } }
            };
            incrementScheduleCounter();

            MOCK_EVENT_DB = new Dictionary<int, Event>
            {
                { 1, new Event { ID = 1, StartTime = DateTime.Now.Date, EndTime = DateTime.Now.Date.AddHours(1), Name = "Example Event", ScheduleId = MOCK_SCHEDULE_DB[1].ID } }
            };
            incrementEventCounter();
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
                if (s.StartTime?.Date.CompareTo(schedule.StartTime?.Date) == 0 &&
                    s.Name.Equals(schedule.Name)) {
                    throw new Exception("Identical schedule already exists.");
                }    
            }

            schedule.ID = scheduleIdCounter;
            MOCK_SCHEDULE_DB.Add(scheduleIdCounter, schedule);
            incrementScheduleCounter();
        }

        /// <inheritdoc/>
        public void UpdateSchedule(Schedule schedule)
        {
            Schedule? attachedSchedule = default;

            foreach (Schedule s in MOCK_SCHEDULE_DB.Values) 
            {
                if (s.ID == schedule.ID)
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
            MOCK_SCHEDULE_DB.Remove(schedule.ID);
            MOCK_SCHEDULE_DB.Add(scheduleIdCounter, schedule);
            incrementScheduleCounter();
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
            MOCK_SCHEDULE_DB.Values.FirstOrDefault(s => s.ID == id);

        /// <inheritdoc/>
        public IEnumerable<Schedule> GetSchedulesByDate(DateTime date) {

            IList<Schedule> result = new List<Schedule>();

            foreach (Schedule s in MOCK_SCHEDULE_DB.Values)
            {
                if (s.StartTime?.Date.CompareTo(date.Date) == 0)
                {
                    result.Add(s);
                }
            }

            return result;
        }

        /// <inheritdoc/>
        public void AddEvent(Event Event)
        {
            // Our idempotency key is the Date (excluding the time component) of our Event + the
            // Schedule ID. The Event ID is just a unique identifer for a row. So if this idempotency key is
            // violated with this new Event addition, throw an exception.
            foreach (Event e in MOCK_EVENT_DB.Values)
            {
                if (eventsOverlap(e, Event) &&
                    e.ScheduleId == Event.ScheduleId)
                {
                    throw new Exception($"Overlapping Event for Schedule with ID {Event.ScheduleId} was detected. Events for a single schedule cannot overlap.");
                }
            }

            Event.ID = eventIDCounter;
            MOCK_EVENT_DB.Add(eventIDCounter, Event);
            incrementEventCounter();
        }

        /// <inheritdoc/>
        public void UpdateEvent(Event Event)
        {
            Event? attachedEvent= default;

            foreach (Event e in MOCK_EVENT_DB.Values)
            {
                if (e.ID == Event.ID)
                {
                    attachedEvent = e;
                    break;
                }
            }

            if (attachedEvent == default)
            {
                AddEvent(Event);
                return;
            }

            // Check that Event overlap rules are not violated
            foreach (Event e in MOCK_EVENT_DB.Values) {
                if (e.ID != Event.ID && e.ScheduleId == Event.ScheduleId &&
                    eventsOverlap(e, Event)) {
                    throw new Exception($"Overlapping Event for Schedule with ID {Event.ScheduleId} was detected. Events for a single schedule cannot overlap.");
                } 
            }

            Event.FillEmptyFields(attachedEvent);
            MOCK_EVENT_DB.Remove(Event.ID);
            MOCK_EVENT_DB.Add(Event.ID, Event);
        }

        /// <inheritdoc/>
        public void DeleteEvent(int id)
        {
            if (!MOCK_EVENT_DB.ContainsKey(id))
            {
                throw new Exception($"Event with id {id} does not exist. Delete action canceled.");
            }

            MOCK_EVENT_DB.Remove(id);
        }

        /// <inheritdoc/>
        public IEnumerable<Event> GetAllEvents() => MOCK_EVENT_DB.Values;

        /// <inheritdoc/>
        public Event? GetEventById(int id) =>
            MOCK_EVENT_DB.Values.FirstOrDefault(e => e.ID == id);

        /// <inheritdoc/>
        public IEnumerable<Event> GetEventsBySchedule(Schedule schedule)
        {
            IList<Event> result = new List<Event>();

            foreach (Event e in MOCK_EVENT_DB.Values) {
                if (e.ScheduleId == schedule.ID) {
                    result.Add(e);
                }
            }

            return result.OrderBy(e => e.StartTime);
        }
        #endregion Public Methods

        #region Private Methods
        private void incrementScheduleCounter() => scheduleIdCounter++;

        private void incrementEventCounter() => eventIDCounter++;

        private bool eventsOverlap(Event e1, Event e2) =>
            
            (e1.StartTime?.Date.CompareTo(e2.StartTime?.Date) == 0 &&
            e1.StartTime?.TimeOfDay.CompareTo(e2.EndTime?.TimeOfDay) == -1) &&
            (e1.EndTime?.Date.CompareTo(e2.EndTime?.Date) == 0 &&
            e1.EndTime?.TimeOfDay.CompareTo(e2.StartTime?.TimeOfDay) == 1);
        #endregion Private Methods
    }
}

