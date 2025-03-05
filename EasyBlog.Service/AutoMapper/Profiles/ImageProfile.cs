using AutoMapper;
using EasyBlog.Entity.DTOs.Images;
using EasyBlog.Entity.Entities;

namespace EasyBlog.Service.AutoMapper.Profiles;

public class ImageProfile : Profile
{
    public ImageProfile()
    {
        CreateMap<ImageDto, Image>().ReverseMap();
    }
}