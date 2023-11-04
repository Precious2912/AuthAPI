using System.ComponentModel.DataAnnotations;

namespace AuthService.Core.Models
{
    public class UserRegistrationModel
    {
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(1)]
        public string Gender { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(100)]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
