using AuthService.Core.Interfaces;
using AuthService.Core.Models;
using AuthService.Core.Services;
using AuthService.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AuthService.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController: ControllerBase
    {
        private readonly IAuthenticationService _service;
        public AuthController(IAuthenticationService service)
        {
            _service = service; 
        }
        [HttpPost("register")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(Response<string>))]
        public async Task<IActionResult> Register([FromBody] UserRegistrationModel registrationModel)
        {
            var response = await _service.Register(registrationModel);
            return Ok(response);
        }

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] UserLoginModel loginModel)
        //{

        //}
    }
}
