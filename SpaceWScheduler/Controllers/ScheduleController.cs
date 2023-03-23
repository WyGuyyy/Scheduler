using Microsoft.AspNetCore.Mvc;
using SpaceWScheduler.Models.Models;
using SpaceWScheduler.Services.Interfaces;

namespace SpaceWScheduler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScheduleController : ControllerBase
    {

        private readonly ILogger<ScheduleController> _logger;
        private readonly IScheduleGetter _scheduleGetter;
        private readonly IScheduleUpdater _scheduleUpdater;

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
            _scheduleGetter.GetSchedule(scheduleId);

        [HttpGet]
        [Route("date/{date}")]
        public ActionResult<IEnumerable<Schedule>> GetScheduleForDate(DateTime date) =>
            _scheduleGetter.GetSchedulesByDate(date).ToList();

        [HttpPost]
        [Route("create")]
        public ActionResult CreateSchedule([FromBody] Schedule schedule)
        {
            _scheduleUpdater.AddSchedule(schedule);
            return CreatedAtAction(nameof(GetSchedule), new { scheduleId = "" + schedule.ID }, schedule);
        }

       /* [HttpPut]
        [Route("update")]
        public ActionResult UpdateSchedule([FromBody] Schedule schedule)
        {
            _scheduleUpdater.UpdateSchedule(schedule);
            return Upate(nameof(CreateSchedule));
        }

        [HttpDelete]
        [Route("delete/{scheduleId}")]
        public ActionResult DeleteSchedule([FromRoute] int scheduleId)
        {
            _scheduleUpdater.DeleteSchedule(scheduleId);
            return CreatedAtAction(nameof(GetSchedule));
        }*/
    }
}