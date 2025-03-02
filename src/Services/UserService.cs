using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> RegisterUserAsync(RegisterDto registerDto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
            return null;

        var newUser = new User
        {
            Email = registerDto.Email,
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

    public async Task<bool> UpdateUserProfileAsync(int userId, UserDto updatedUser)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        user.Username = !string.IsNullOrWhiteSpace(updatedUser.Username) ? updatedUser.Username : user.Username;
        user.ProfileImageUrl = updatedUser.ProfileImageUrl ?? user.ProfileImageUrl;
        user.Birthday = updatedUser.Birthday ?? user.Birthday;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}