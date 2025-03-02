using AutoMapper;
using EasyBlog.Entity.Entities;
using EasyBlog.Entity.DTOs.Articles;

namespace EasyBlog.Service.AutoMapper.Articles;

public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<ArticleDto, Article>().ReverseMap();
        CreateMap<ArticleAddDto, Article>().ReverseMap();
        CreateMap<ArticleListDto, Article>().ReverseMap();
        CreateMap<ArticleUpdateDto, Article>().ReverseMap();
    }
}