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
    public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
    {
        var eventList = await _context.Events
            .Include(e => e.User)
            .Include(e => e.Category)
            .Select(e => new EventDto
            {
                Title = e.Title,
                Description = e.Description,
                Date = e.Date,
                CreatedAt = e.CreatedAt,
                User = new UserDto
                {
                    Id = e.User.Id,
                    Username = e.User.Username,
                    ProfileImageUrl = e.User.ProfileImageUrl
                },
                Category = new CategoryDto
                {
                    Id = e.Category.Id,
                    Name = e.Category.Name
                }
            })
            .ToListAsync();

        return eventList;
    }

    // 특정 이벤트 조회
    public async Task<Event?> GetEventByIdAsync(int id)
    {
        return await _context.Events.FindAsync(id);
    }

    // 이벤트 생성
    public async Task<Event> CreateEventAsync(EventDto eventDto)
    {
        var newEvent = new Event
        {
            UserId = eventDto.UserId,
            CategoryId = eventDto.CategoryId,
            Title = eventDto.Title ?? "",
            Description = eventDto.Description,
            Date = eventDto.Date,
            CreatedAt = eventDto.CreatedAt
        };

        _context.Events.Add(newEvent);
        await _context.SaveChangesAsync();
        return newEvent;
    }
}