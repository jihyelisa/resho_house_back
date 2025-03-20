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
            return Ok(new { success = false, message = "Invalid email or password" });
        }

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            // Secure = true,
            // SameSite = SameSiteMode.Strict,
            Secure = false,
            SameSite = SameSiteMode.Lax
        };
        Response.Cookies.Append("jwt", token, cookieOptions);  // ✅ 쿠키에 JWT 저장

        return Ok(new { success = true, message = "Login successful" });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        Response.Cookies.Delete("jwt");
        return Ok(new { success = true, message = "Logged out successfully" });
    }
}