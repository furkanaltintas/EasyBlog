using AutoMapper;
using EasyBlog.Data.UnitOfWorks;
using EasyBlog.Entity.DTOs.Articles;
using EasyBlog.Entity.Entities;
using EasyBlog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EasyBlog.Service.Services.Concretes;

public class ArticleService : BaseService, IArticleService
{
    private readonly ICurrentUserService _currentUserService;

    public ArticleService(IMapper mapper, IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : base(mapper, unitOfWork) { _currentUserService = currentUserService; }

    public async Task CreateArticleAsync(ArticleAddDto articleAddDto)
    {
        var userId = _currentUserService.GetCurrentUserId();
        if (userId == Guid.Empty)
            throw new UnauthorizedAccessException("Kullanıcı kimliği bulunamadı");

        var article = _mapper.Map<Article>(articleAddDto);
        article.UserId = userId;

        await _unitOfWork.GetRepository<Article>().AddAsync(article);
        await _unitOfWork.SaveAsync();
    }

    public async Task<List<ArticleListDto>> GetAllArticlesWithCategoryNonDeletedAsync()
    {
        var articles = await _unitOfWork
            .GetRepository<Article>()
            .GetAllAsync(a => !a.IsDeleted, a => a.Category);

        return _mapper.Map<List<ArticleListDto>>(articles);
    }
}