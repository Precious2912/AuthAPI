using AuthService.Core.Entities;
using AuthService.Core.Models.Dto;
using AutoMapper;

namespace AuthService.Core
{
    public class Automapper: Profile
    {
        public Automapper() 
        {
            CreateMap<User, UserDto>().ReverseMap();

        }
    }
}
