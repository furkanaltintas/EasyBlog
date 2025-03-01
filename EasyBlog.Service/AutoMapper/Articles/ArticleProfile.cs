﻿using AutoMapper;
using EasyBlog.Entity.DTOs.Articles;
using EasyBlog.Entity.Entities;

namespace EasyBlog.Service.AutoMapper.Articles;

public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<ArticleDto, Article>().ReverseMap();
    }
}