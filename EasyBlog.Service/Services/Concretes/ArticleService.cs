using AutoMapper;
using EasyBlog.Entity.Entities;
using EasyBlog.Data.UnitOfWorks;
using EasyBlog.Entity.DTOs.Articles;
using EasyBlog.Service.Services.Abstractions;
using EasyBlog.Entity.DTOs.Categories;

namespace EasyBlog.Service.Services.Concretes;

public class ArticleService : BaseService, IArticleService
{
    public ArticleService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork) { }

    public async Task CreateArticleAsync(ArticleAddDto articleAddDto)
    {
        // Interceptor içerisinde yapılıyor
        //var userId = _currentUserService.GetCurrentUserId();
        //if (userId == Guid.Empty)
        //    throw new UnauthorizedAccessException("Kullanıcı kimliği bulunamadı");



        var article = _mapper.Map<Article>(articleAddDto);

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

    public async Task<ArticleUpdateDto> GetArticleForUpdateAsync(Guid articleId)
    {
        var article = await _unitOfWork.GetRepository<Article>().GetAsync(a => a.Id == articleId && !a.IsDeleted);
        var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync(c => !c.IsDeleted);

        var articleUpdateDto = _mapper.Map<ArticleUpdateDto>(article);
        articleUpdateDto.Categories = _mapper.Map<List<CategoryDto>>(categories);
        return articleUpdateDto;
    }

    public async Task<ArticleDto> GetArticleWithCategoryNonDeletedAsync(Guid articleId) =>
        _mapper.Map<ArticleDto>(await GetArticleAsync(articleId));

    public async Task SafeDeleteArticleAsync(Guid articleId)
    {
        var article = await GetArticleAsync(articleId);

        await _unitOfWork.GetRepository<Article>().DeleteAsync(article);
        await _unitOfWork.SaveAsync();
    }

    public async Task<bool> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto)
    {
        var article = await GetArticleAsync(articleUpdateDto.Id);

        if (article == null)
            return false;

        _mapper.Map(articleUpdateDto, article);
        await _unitOfWork.GetRepository<Article>().UpdateAsync(article);
        await _unitOfWork.SaveAsync();
        return true;
    }



    private async Task<Article> GetArticleAsync(Guid articleId) =>
        await _unitOfWork.GetRepository<Article>().GetAsync(a => !a.IsDeleted && a.Id == articleId, a => a.Category);
}