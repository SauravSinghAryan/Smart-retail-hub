using System.ComponentModel.DataAnnotations;

namespace hardwareAPI.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
