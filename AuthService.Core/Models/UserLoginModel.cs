using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace AuthService.Core.Models
{
    public class UserLoginModel
    {
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
    }
}
