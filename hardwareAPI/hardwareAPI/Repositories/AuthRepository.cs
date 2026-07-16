using hardwareAPI.Data;
using hardwareAPI.DTOs;
using hardwareAPI.Interfaces;
using hardwareAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace hardwareAPI.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<User> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                Name = registerDto.Name,
                Username = registerDto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = registerDto.Role
            };

            await _context.Users.AddAsync(user);

            return user;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}