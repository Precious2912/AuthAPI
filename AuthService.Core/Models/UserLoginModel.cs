using System.ComponentModel.DataAnnotations;

namespace AuthService.Core.Models
{
    public class UserLoginModel
    {
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
