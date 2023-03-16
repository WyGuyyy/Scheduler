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

        public ScheduleController(ILogger<ScheduleController> logger, IScheduleGetter scheduleGetter)
        {
            _logger = logger;
            _scheduleGetter = scheduleGetter;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Schedule>> GetSchedules() =>
            _scheduleGetter.GetSchedules().ToList();

        [HttpGet]
        [Route("{scheduleId}")]
        public ActionResult<Schedule> GetSchedule(int scheduleId) =>
            _scheduleGetter.GetSchedule(scheduleId);

        [HttpGet]
        [Route("date/{date}")]
        public ActionResult<IEnumerable<Schedule>> GetScheduleForDate(DateTime date) =>
            _scheduleGetter.GetScheduleByDate(date).ToList();
    }
}