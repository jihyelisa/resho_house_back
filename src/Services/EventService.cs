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
    public async Task<IEnumerable<EventDto>> GetEventListAsync(string searchType, string? searchText, string orderBy)
    {
        var eventList = await _context.Events
            .Include(e => e.User)
            .Include(e => e.Category)
            .ToListAsync();
        
        if (!string.IsNullOrEmpty(searchText) && eventList != null)
        {
            switch (searchType)
            {
                case "Title":
                    eventList = eventList.Where(e => e.Title.Contains(searchText)).ToList();
                    break;
                
                case "Writer":
                    eventList = eventList.Where(e => e.User != null &&
                                                     e.User.Username != null &&
                                                     e.User.Username.Contains(searchText)).ToList();
                    break;
                
                case "Category":
                    eventList = eventList.Where(e => e.Category != null &&
                                                     e.Category.Name.Contains(searchText)).ToList();
                    break;
                
                // case "Participant":
                //     eventList = eventList.Where(e => e.Participants.Contains(/* ? */))
                //         .ToList();
                //     break;

                default:
                    break;
            }
        }

        if (eventList == null)
            return [];
        
        switch (orderBy)
        {
            case "WrittenDateDesc":
                eventList = eventList.OrderByDescending(e => e.CreatedAt).ToList();
                break;
            case "WrittenDateAsc":
                eventList = eventList.OrderBy(e => e.CreatedAt).ToList();
                break;
            case "EventDateDesc":
                eventList = eventList.OrderByDescending(e => e.Date).ToList();
                break;
            case "EventDateAsc":
                eventList = eventList.OrderBy(e => e.Date).ToList();
                break;
        }

        var eventListWithImages = eventList.Select(e => new EventDto
        {
            Title = e.Title,
            Content = e.Content,
            Date = e.Date,
            Participants = e.Participants,
            CreatedAt = e.CreatedAt,
            User = e.User != null ? new UserDto
            {
                Id = e.User.Id,
                Username = e.User.Username,
                ProfileImageUrl = e.User.ProfileImageUrl
            } : null,
            Category = e.Category != null ? new CategoryDto
            {
                Id = e.Category.Id,
                Name = e.Category.Name
            } : null,
            EventImageList = _context.EventImages.ToList()
        }).ToList();

        return eventListWithImages;
    }

    public async Task<EventDto?> GetEventByIdAsync(int eventId)
    {
        var eventItem = await _context.Events
            .Include(e => e.User)
            .Include(e => e.Category)
            .Where(e => e.Id == eventId)
            .FirstOrDefaultAsync();
        
        if (eventItem == null)
        {
            return null;
        }

        var eventItemWithImages = new EventDto
            {
                Title = eventItem.Title,
                Content = eventItem.Content,
                Date = eventItem.Date,
                Participants = eventItem.Participants,
                CreatedAt = eventItem.CreatedAt,
                User = eventItem.User != null ? new UserDto
                {
                    Id = eventItem.User.Id,
                    Username = eventItem.User.Username,
                    ProfileImageUrl = eventItem.User.ProfileImageUrl
                } : null,
                Category = eventItem.Category != null ? new CategoryDto
                {
                    Id = eventItem.Category.Id,
                    Name = eventItem.Category.Name
                } : null,
                EventImageList = _context.EventImages
                    .Where(ei => ei.EventId == eventItem.Id)
                    .ToList()
            };
            
        return eventItemWithImages;
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