using AuthService.Core.Models;
using AuthService.Core.Models.Dto;
using AuthService.Models;

namespace AuthService.Core.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Response<string>> Register(UserRegistrationModel request);
        Task<Response<string>> Login(UserLoginModel request);
        Task<Response<List<UserDto>>> GetUsers();
    }
}
