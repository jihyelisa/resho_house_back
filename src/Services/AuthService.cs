using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
        // 1️⃣ 사용자 정보 확인 (이메일로 검색)
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
        if (user == null) return null;  // 사용자 없음

        // 2️⃣ 비밀번호 검증 (실제 환경에서는 암호화된 비밀번호를 비교해야 함)
        if (user.PasswordHash != loginDto.Password)
        {
            return null; // 비밀번호 불일치
        }

        // 3️⃣ JWT 토큰 생성
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"] ?? "defaultSecretKey");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                //new Claim("Username", user.Username)
            }),
            Expires = DateTime.UtcNow.AddHours(2), // 토큰 만료 시간 설정
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token); // JWT 토큰 반환
    }
}