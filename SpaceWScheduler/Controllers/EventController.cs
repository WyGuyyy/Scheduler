using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaceWScheduler.Models.Interfaces;
using SpaceWScheduler.Models.Models;

namespace SpaceWScheduler.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private readonly ILogger<EventController> _logger;
        private readonly IEventGetter _eventGetter;
        private readonly IEventUpdater _eventUpdater;

        public EventController(
            ILogger <EventController> logger,
            IEventGetter eventGetter,
            IEventUpdater eventUpdater
        ) 
        {
            _logger = logger;
            _eventGetter = eventGetter;
            _eventUpdater = eventUpdater;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Event>> GetAllEvents() =>
            _eventGetter.GetEvents().ToList();
        

        [HttpGet]
        [Route("{eventId}")]
        public ActionResult<Event?> GetEvent([FromRoute] int eventId) =>
            _eventGetter.GetEvent(eventId);


    }
}
