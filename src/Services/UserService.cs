using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> RegisterUserAsync(RegisterDto registerDto)
    {
        if (!IsValidEmail(registerDto.Email) ||
            await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
        {
            return null;
        }

        var newUser = new User
        {
            Email = registerDto.Email,
            ProfileImageUrl = registerDto.ProfileImageUrl,
            Username = registerDto.Email.Split('@')[0],
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return new UserDto
        {
            Id = newUser.Id,
            Email = newUser.Email,
            Username = newUser.Username
        };
    }

    public async Task<UserDto?> GetUserProfileAsync(int userId)
    {
        var UserItem = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
        if (UserItem == null) return null;

        return new UserDto
        {
            Id = UserItem.Id,
            ProfileImageUrl = UserItem.ProfileImageUrl,
            Email = UserItem.Email,
            Username = UserItem.Username,
            Birthday = UserItem.Birthday
        };
    }

    public async Task<bool> UpdateUserProfileAsync(int userId, User updatedUser)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        updatedUser.Username = HttpUtility.HtmlEncode(updatedUser.Username);
        user.Username = !string.IsNullOrWhiteSpace(updatedUser.Username) ? updatedUser.Username : user.Username;

        var passwordHash = !string.IsNullOrWhiteSpace(updatedUser.PasswordHash)
            ? BCrypt.Net.BCrypt.HashPassword(updatedUser.PasswordHash)
            : null;
        user.PasswordHash = !string.IsNullOrWhiteSpace(passwordHash) ? passwordHash : user.PasswordHash;
        
        user.ProfileImageUrl = updatedUser.ProfileImageUrl ?? user.ProfileImageUrl;
        user.Birthday = updatedUser.Birthday ?? user.Birthday;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public bool IsValidEmail(string email)
    {
        var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, emailPattern);
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}