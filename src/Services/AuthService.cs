using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<string?> Authenticate(LoginDto loginDto)
    {
        // 1️⃣ 이메일 소문자로 통일 (대소문자 구분 문제 방지)
        var normalizedEmail = loginDto.Email.ToLower();

        // 2️⃣ 유저 찾기
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail);
        if (user == null) return null;

        // 3️⃣ 비밀번호 검증
        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            return null;
        }

        // 로그인 시간 로깅
        user.SignedInAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        // 3️⃣ JWT 토큰 생성
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"] ?? "defaultSecretKey");
        var issuer = _configuration["Jwt:Issuer"] ?? "http://localhost:5232";
        var audience = _configuration["Jwt:Audience"] ?? "http://localhost:5232";

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}