using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    // ✅ 로그인 API (POST: api/auth/login)
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var token = await _authService.Authenticate(loginDto);

        if (token == null)
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }

        return Ok(new { token });
    }
}