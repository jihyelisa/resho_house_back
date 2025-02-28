using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class EventService
{
    private readonly AppDbContext _context;

    public EventService(AppDbContext context)
    {
        _context = context;
    }

    // 전체 이벤트 조회
    public async Task<IEnumerable<Event>> GetAllEventsAsync()
    {
        return await _context.events.ToListAsync();
    }

    // 특정 이벤트 조회
    public async Task<Event?> GetEventByIdAsync(int id)
    {
        return await _context.events.FindAsync(id);
    }

    // 이벤트 생성
    public async Task<Event> CreateEventAsync(EventDto eventDto)
    {
        var newEvent = new Event
        {
            user_id = eventDto.user_id,
            category_id = eventDto.category_id,
            title = eventDto.title,
            description = eventDto.description,
            created_at = eventDto.created_at
        };

        _context.events.Add(newEvent);
        await _context.SaveChangesAsync();
        return newEvent;
    }
}