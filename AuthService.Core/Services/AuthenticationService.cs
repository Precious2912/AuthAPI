using AuthService.Core.Entities;
using AuthService.Core.Interfaces;
using AuthService.Core.Models;
using AuthService.Entities;
using AuthService.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Core.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public AuthenticationService(AppDbContext context, IConfiguration config) 
        { 
            _context = context;
            _config = config;
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
                    Message = "User created successfully"
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

        public async Task<Response<string>> Login(UserLoginModel request)
        {
            //Find the user by username 
            var user = _context.Users.SingleOrDefault(x => x.Username == request.Username);

            if (user == null)
            {
                return new Response<string> { Status = Constants.STATUS_FAIL, Message = "Please kindly register"};
            }

            //verify the password
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return new Response<string> { Status = Constants.STATUS_FAIL, Message = "Invalid credentials" };
            }

            //Generate token
            var token = GenerateToken(user, _config);

            //return token
            return new Response<string>
            {
                Status = Constants.STATUS_SUCCESS,
                Message = "Login successful",
                Data = token,
            };
        }

        private static string GenerateToken(User user, IConfiguration config)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var token = new JwtSecurityToken(
                config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddSeconds(Convert.ToInt32(config["Jwt:ExpirationInSeconds"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
