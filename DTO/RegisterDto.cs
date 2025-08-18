using System.ComponentModel.DataAnnotations;

namespace EcommerceTrail.DTO
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string PasswordHash { get; set; } = null!;
    }
}
