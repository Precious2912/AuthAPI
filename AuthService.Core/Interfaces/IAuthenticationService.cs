using AuthService.Core.Models;
using AuthService.Models;

namespace AuthService.Core.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Response<string>> Register(UserRegistrationModel request);
    }
}
