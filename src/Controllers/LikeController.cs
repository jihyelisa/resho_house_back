using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;

[Route("api/likes")]
[ApiController]
[Authorize] // 인증된 사용자만 접근 가능
public class LikeController : ControllerBase
{
    private readonly LikeService _likeService;

    public LikeController(LikeService likeService)
    {
        _likeService = likeService;
    }

    [HttpGet("event/{eventId}")]
    public async Task<ActionResult<IEnumerable<int>>> GetLikesByEventId(int eventId)
    {
        var likes = await _likeService.GetLikesByEventIdAsync(eventId);
        return Ok(likes);
    }

    [HttpGet("comment/{commentId}")]
    public async Task<ActionResult<IEnumerable<int>>> GetLikesByCommentId(int commentId)
    {
        var likes = await _likeService.GetLikesByCommentId(commentId);
        return Ok(likes);
    }

    [HttpPost("create")]
    public async Task<ActionResult<Like>> CreateLike([FromBody] Like like)
    {
        var newLike = await _likeService.CreateLike(like);
        return Ok(newLike);
    }

    [HttpDelete("delete/{likeId}")]
    public async Task<ActionResult<Like>> DeleteLike([FromBody] Like like)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var success = await _likeService.DeleteLike(like);
        return success ? Ok(new { message = "All notifications deleted" }) : NotFound();
    }
}