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
        public async Task<ActionResult<IEnumerable<Event>>> GetAllEvents() =>
            (await _eventGetter.GetEvents()).ToList();
        

        [HttpGet]
        [Route("{eventId}")]
        public async Task<ActionResult<Event?>> GetEvent([FromRoute] int eventId) =>
            await _eventGetter.GetEvent(eventId);

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateEvent([FromBody] Event Event)
        {
            await _eventUpdater.AddEvent(Event);
            return CreatedAtAction(nameof(GetEvent), new { EventId = Event.ID }, Event);
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> UpdateEvent([FromBody] Event Event)
        {
            await _eventUpdater.UpdateEvent(Event);
            return CreatedAtAction(nameof(GetEvent), new { eventId = Event.ID }, Event);
        }

        [HttpDelete]
        [Route("delete/{eventId}")]
        public async Task<ActionResult> DeleteEvent([FromRoute] int eventId) {
            await _eventUpdater.DeleteEvent(eventId);
            return NoContent();
        }
    }
}
