using AutoMapper;
using EasyBlog.Entity.DTOs.Categories;
using EasyBlog.Entity.Entities;

namespace EasyBlog.Service.AutoMapper.Categories;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryDto, Category>().ReverseMap();
    }
}