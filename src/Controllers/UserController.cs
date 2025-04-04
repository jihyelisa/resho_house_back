using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    // 1️⃣ Register User (POST /api/users/register)
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var user = await _userService.RegisterUserAsync(registerDto);
        if (user == null)
            return BadRequest(new { message = "Email is already in use." });

        return CreatedAtAction(nameof(GetProfile), new { id = user.Id }, user);
    }

    // 2️⃣ Get User Profile (GET /api/users/me)
    [HttpGet("me")]
    public async Task<IActionResult> GetProfile()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdClaim, out int userId))
        {
            return Unauthorized(new { message = "Invalid user ID" });
        }

        var user = await _userService.GetUserProfileAsync(userId);
        return user != null ? Ok(user) : NotFound(new { message = "User not found" });
    }

    // 3️⃣ Update User Profile (PUT /api/users/update)
    [Authorize]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateProfile([FromBody] User updatedUser)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var success = await _userService.UpdateUserProfileAsync(userId, updatedUser);
        return success ? Ok(new { message = "Profile updated successfully." }) : Ok(new { message = "Failed to update." });
    }
}