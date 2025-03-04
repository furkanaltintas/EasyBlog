using AutoMapper;
using EasyBlog.Entity.DTOs.Users;
using EasyBlog.Entity.Entities;

namespace EasyBlog.Service.AutoMapper.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserListDto, AppUser>().ReverseMap();
        CreateMap<UserDto, AppUser>().ReverseMap();
        CreateMap<UserAddDto, AppUser>().ReverseMap();
        CreateMap<UserUpdateDto, AppUser>().ReverseMap();
        CreateMap<UserProfileDto, AppUser>()
            .ForMember(dest => dest.ImageId, opt => opt.Condition(src => src.Image != null))
            .ReverseMap(); // Eğer Photo null ise ImageId'yi güncelleme
    }
}