using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/events")]
[ApiController]
[Authorize]
public class EventController : ControllerBase
{
    private readonly EventService _eventService;

    public EventController(EventService eventService)
    {
        _eventService = eventService;
    }

    // 1️⃣ 전체 이벤트 조회 (POST: api/events)
    [HttpPost]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetEvents(string searchType, string? searchText, string orderBy)
    {
        var events = await _eventService.GetEventListAsync(searchType, searchText, orderBy);
        return Ok(events);
    }

    // 2️⃣ 특정 이벤트 조회 (GET: api/events/{id})
    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetEventById(int id)
    {
        var eventItem = await _eventService.GetEventByIdAsync(id);
        if (eventItem == null)
        {
            return NotFound();
        }
        return Ok(eventItem);
    }

    // 3️⃣ 새로운 이벤트 생성 (POST: api/events/create)
    [HttpPost("create")]
    public async Task<ActionResult<Event>> CreateEvent(EventDto eventDto)
    {
        var newEvent = await _eventService.CreateEventAsync(eventDto);
        return CreatedAtAction(nameof(GetEventById), new { Id = newEvent.Id }, newEvent);
    }
}