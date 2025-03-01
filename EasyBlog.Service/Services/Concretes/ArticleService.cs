using AutoMapper;
using EasyBlog.Data.UnitOfWorks;
using EasyBlog.Entity.DTOs.Articles;
using EasyBlog.Entity.Entities;
using EasyBlog.Service.Services.Abstractions;

namespace EasyBlog.Service.Services.Concretes;

public class ArticleService : IArticleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ArticleService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }



    public async Task<List<ArticleDto>> GetAllArticlesAsync()
    {
        var articles = await _unitOfWork.GetRepository<Article>().GetAllAsync();
        var articleDtos = _mapper.Map<List<ArticleDto>>(articles);
        return articleDtos;
    }
}