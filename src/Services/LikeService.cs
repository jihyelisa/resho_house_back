using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class LikeService
{
    private readonly AppDbContext _context;

    public LikeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<int>> GetLikesByEventIdAsync(int eventId)
    {
        var likes = await _context.Likes
            .Where(l => l.EventId == eventId)
            .Select(l => l.UserId)
            .ToListAsync();

        return likes;
    }

    public async Task<IEnumerable<int>> GetLikesByCommentId(int eventId)
    {
        var likes = await _context.Likes
            .Where(l => l.CommentId == eventId)
            .Select(l => l.UserId)
            .ToListAsync();

        return likes;
    }

    public async Task<Like> CreateLike(Like like)
    {
        var newLike = new Like
        {
            UserId = like.UserId,
            EventId = like.EventId,
            CommentId = like.CommentId
        };

        _context.Likes.Add(like);
        await _context.SaveChangesAsync();
        return newLike;
    }

    public async Task<bool> DeleteLike(Like like)
    {
        var likeToBeRemoved = _context.Likes.Where(l => l.UserId == like.UserId &&
                                                       (l.EventId == like.EventId || l.CommentId == like.CommentId)).FirstOrDefault();
        if (likeToBeRemoved == null) return false;

        _context.Likes.Remove(likeToBeRemoved);
        await _context.SaveChangesAsync();
        return true;
    }
}