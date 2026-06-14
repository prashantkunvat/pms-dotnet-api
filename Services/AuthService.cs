using Microsoft.EntityFrameworkCore;
using PMS.Api.Data;
using PMS.Api.DTOs.Auth;
using PMS.Api.Interfaces;
using PMS.Api.Models;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IJwtService _jwtService;

    public AuthService(
        AppDbContext context,
        IJwtService jwtService
    )
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto> RegisterAsync(
        RegisterDto dto
    )
    {
        var emailExists = await _context.Users
            .AnyAsync(u => u.Email == dto.Email);

        if(emailExists)
        {
            throw new Exception("Email already exists.");
        }

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,

            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        var token = _jwtService.GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token
        };
    }

    public async Task<AuthResponseDto> LoginAsync(
        LoginDto dto
    )
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(
                u => u.Email == dto.Email
            );

        if(user is null)
        {
            return null;
        }

        var ValidPassword = 
            BCrypt.Net.BCrypt.Verify(
                dto.Password,
                user.PasswordHash
            );

        if(!ValidPassword)
        {
            return null;
        }

        var token = _jwtService.GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token
        };
    }
}