using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetEventByIdAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    // 이벤트 생성
    public async Task<Event> CreateEventAsync(EventDto eventDto)
    {
        var newEvent = new Event
        {
            UserId = eventDto.UserId,
            CategoryId = eventDto.CategoryId,
            Title = eventDto.Title ?? "",
            Content = eventDto.Content,
            Date = eventDto.Date,
            EventImageList = eventDto.EventImageList
        };

        _context.Events.Add(newEvent);
        await _context.SaveChangesAsync();
        return newEvent;
    }
}