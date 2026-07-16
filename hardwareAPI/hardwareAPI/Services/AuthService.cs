using hardwareAPI.DTOs;
using hardwareAPI.Helpers;
using hardwareAPI.Interfaces;
using hardwareAPI.Models;

namespace hardwareAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly JwtTokenService _jwtTokenService;

        public AuthService(
            IAuthRepository authRepository,
            JwtTokenService jwtTokenService)
        {
            _authRepository = authRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _authRepository.GetUserByUsernameAsync(registerDto.Username);

            if (existingUser != null)
            {
                throw new Exception("Username already exists.");
            }

            var user = await _authRepository.RegisterAsync(registerDto);

            await _authRepository.SaveChangesAsync();

            return new AuthResponseDto
            {
                Message = "Registration Successful",
                Token = _jwtTokenService.GenerateToken(user),
                User = new UserDto
                {
                    Name = user.Name,
                    Username = user.Username,
                    Role = user.Role
                }
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _authRepository.GetUserByUsernameAsync(loginDto.Username);

            if (user == null)
            {
                throw new Exception("Invalid username or password.");
            }

            bool validPassword = BCrypt.Net.BCrypt.Verify(
                loginDto.Password,
                user.PasswordHash);

            if (!validPassword)
            {
                throw new Exception("Invalid username or password.");
            }

            return new AuthResponseDto
            {
                Message = "Login Successful",
                Token = _jwtTokenService.GenerateToken(user),
                User = new UserDto
                {
                    Name = user.Name,
                    Username = user.Username,
                    Role = user.Role
                }
            };
        }
    }
}