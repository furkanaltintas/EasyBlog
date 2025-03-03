using AutoMapper;
using EasyBlog.Entity.DTOs.Categories;
using EasyBlog.Entity.Entities;

namespace EasyBlog.Service.AutoMapper.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryDto, Category>().ReverseMap();
        CreateMap<CategoryAddDto, Category>().ReverseMap();
        CreateMap<CategoryUpdateDto, Category>().ReverseMap();
        CreateMap<CategoryListDto, Category>().ReverseMap();
    }
}