using AutoMapper;
using EasyBlog.Entity.DTOs.Articles;
using EasyBlog.Entity.Entities;

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