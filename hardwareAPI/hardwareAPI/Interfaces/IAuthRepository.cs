using hardwareAPI.DTOs;
using hardwareAPI.Models;

namespace hardwareAPI.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);

        Task<User> RegisterAsync(RegisterDto registerDto);

        Task SaveChangesAsync();
    }
}