using System.ComponentModel.DataAnnotations;

namespace EcommerceTrail.DTO
{
    public class ResetPassword
    {

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; }= null!;

        [Required]

        public string NewPassword { get; set; }=null!;
    }
}
