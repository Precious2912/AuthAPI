using AuthService.Core.Entities;
using AuthService.Core.Interfaces;
using AuthService.Core.Models;
using AuthService.Entities;
using AuthService.Models;

namespace AuthService.Core.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly AppDbContext _context;
        public AuthenticationService(AppDbContext context) 
        { 
            _context = context;
        }

        public async Task<Response<string>> Register(UserRegistrationModel request)
        {
            try
            {
                if (_context.Users.Any(x => x.Username == request.Username))
                {
                    return new Response<string>
                    {
                        Status = Constants.STATUS_FAIL,
                        Message = "Username already exists"
                    };
                }

                //Hash password 
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

                //create new user
                var newUser = new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Gender = request.Gender,
                    PhoneNumber = request.PhoneNumber,
                    PasswordHash = hashedPassword
                };

                //Add user to db
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return new Response<string>
                {
                    Status = Constants.STATUS_SUCCESS,
                    Message = "Username created successfully"
                };

            }
            catch (Exception ex)
            {
                //Log exception
                return new Response<string>
                {
                    Status = Constants.STATUS_ERROR,
                    Message = $"Failed to create user with error - {ex.Message}"
                };
            }
        }
    }
}
