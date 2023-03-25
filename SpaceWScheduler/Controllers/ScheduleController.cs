using Microsoft.AspNetCore.Mvc;
using SpaceWScheduler.Models.Models;
using SpaceWScheduler.Services.Interfaces;
using System.Text.Json.Serialization;

namespace SpaceWScheduler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScheduleController : ControllerBase
    {

        private readonly ILogger<ScheduleController> _logger;
        private readonly IScheduleGetter _scheduleGetter;
        private readonly IScheduleUpdater _scheduleUpdater;

        // NEED TO ENSURE THAT EVENT LIST IS POPULATED TO SCHEDULES!!!
        public ScheduleController(
            ILogger<ScheduleController> logger,
            IScheduleGetter scheduleGetter,
            IScheduleUpdater scheduleUpdater
        )
        {
            _logger = logger;
            _scheduleGetter = scheduleGetter;
            _scheduleUpdater = scheduleUpdater;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Schedule>> GetSchedules() =>
            _scheduleGetter.GetSchedules().ToList();

        [HttpGet]
        [Route("{scheduleId}")]
        public ActionResult<Schedule?> GetSchedule(int scheduleId) =>
            Ok(_scheduleGetter.GetSchedule(scheduleId));

        [HttpGet]
        [Route("date/{date}")]
        public ActionResult<IEnumerable<Schedule>> GetScheduleForDate(DateTime date) =>
            _scheduleGetter.GetSchedulesByDate(date).ToList();

        [HttpPost]
        [Route("create")]
        public ActionResult CreateSchedule([FromBody] ScheduleCreateModel scm)
        {
            Schedule schedule = convertScheduleCreateModelToSchedule(scm);
            _scheduleUpdater.AddSchedule(schedule);
            return CreatedAtAction(nameof(GetSchedule), new { scheduleId = "" + schedule.ID }, schedule);
        }

        [HttpPut]
        [Route("update")]
        public ActionResult UpdateSchedule([FromBody] ScheduleCreateModel scm)
        {
            Schedule schedule = convertScheduleCreateModelToSchedule(scm);
            _scheduleUpdater.UpdateSchedule(schedule);
            return CreatedAtAction(nameof(GetSchedule), new { scheduleId = "" + schedule.ID }, schedule);
        }

        [HttpDelete]
        [Route("delete/{scheduleId}")]
        public ActionResult DeleteSchedule([FromRoute] int scheduleId)
        {
            _scheduleUpdater.DeleteSchedule(scheduleId);
            return NoContent();
        }

        private Schedule convertScheduleCreateModelToSchedule(ScheduleCreateModel scm) =>
            new Schedule { ID = scm.ID, StartTime = scm.StartTime, EndTime = scm.EndTime, Name = scm.Name };

        /// <summary>
        /// Class to restrict the properties able to be set for schedules via the api interface directly
        /// </summary>
        public class ScheduleCreateModel {
            public int ID { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
            public string? Name { get; set; }

            [JsonConstructor]
            public ScheduleCreateModel() { }
        }
    }
}