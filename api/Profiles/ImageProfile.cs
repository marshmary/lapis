using AutoMapper;
using Lapis.API.Dtos;
using Lapis.API.Models;

namespace Lapis.API.Profiles
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            // Read
            CreateMap<Image, ImageReadDto>();
            CreateMap<Size, ImageReadSize>();
            CreateMap<Credit, ImageReadCredit>();

            // Create
            CreateMap<ImageCreateDto, Image>();
            CreateMap<ImageCreateColor, Colors>();
            CreateMap<ImageCreateCredit, Credit>();

            // Update
            CreateMap<ImageUpdateDto, Image>();
            CreateMap<Image, ImageUpdateDto>();
        }
    }
}