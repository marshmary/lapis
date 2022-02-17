using AutoMapper;
using Lapis.API.Dtos;
using Lapis.API.Models;

namespace Lapis.API.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<LoginDto, User>();
            CreateMap<User, LoginReponseDto>();
        }
    }
}