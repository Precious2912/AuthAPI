using System.ComponentModel.DataAnnotations;

namespace AuthService.Core.Models
{
    public class UserRegistrationModel
    {
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        [SwaggerSchemaExample("test")]

        public string FirstName { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        [SwaggerSchemaExample("user")]

        public string LastName { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(1)]
        [SwaggerSchemaExample("M")]

        public string Gender { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        [SwaggerSchemaExample("test_user")]

        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(100)]
        [SwaggerSchemaExample("test_user123XYZ@@")]

        public string Password { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(100)]
        [SwaggerSchemaExample("test_user123XYZ@@")]

        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        [SwaggerSchemaExample("testuser@gmail.com")]

        public string Email { get; set; }

        [Phone]
        [SwaggerSchemaExample("08012345678")]
        public string PhoneNumber { get; set; }
    }
}
