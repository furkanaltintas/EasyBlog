using AutoMapper;
using EasyBlog.Core.Utilities.Results.Abstract;
using EasyBlog.Core.Utilities.Results.ComplexTypes;
using EasyBlog.Core.Utilities.Results.Concrete;
using EasyBlog.Data.UnitOfWorks;
using EasyBlog.Entity.DTOs.Articles;
using EasyBlog.Entity.DTOs.Categories;
using EasyBlog.Entity.Entities;
using EasyBlog.Service.Services.Abstractions;
using EasyBlog.Service.Utilities;

namespace EasyBlog.Service.Services.Concretes;

public class ArticleService : RepositoryService, IArticleService
{
    private readonly ICurrentUserService _currentUserService;

    public ArticleService(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService) : base(mapper, unitOfWork)
    {
        _currentUserService = currentUserService;
    }

    public async Task<IResult> CreateArticleAsync(ArticleAddDto articleAddDto)
    {
        var article = _mapper.Map<Article>(articleAddDto);
        article.UserId = _currentUserService.GetCurrentUserId();

        await _unitOfWork.GetRepository<Article>().AddAsync(article);
        await _unitOfWork.SaveAsync();
        return new Result(ResultStatus.Success, Messages.Article.Add(article.Title));
    }

    public async Task<IDataResult<List<ArticleListDto>>> GetAllArticlesWithCategoryNonDeletedAsync()
    {
        var articles = await _unitOfWork
            .GetRepository<Article>()
            .GetAllAsync(a => !a.IsDeleted, a => a.Category);

        if (articles.Count > -1)
        {
            var articleListDtos = _mapper.Map<List<ArticleListDto>>(articles);
            return new DataResult<List<ArticleListDto>>(ResultStatus.Success, articleListDtos);
        }

        return new DataResult<List<ArticleListDto>>(ResultStatus.Error, null);
    }

    public async Task<IDataResult<ArticleUpdateDto>> GetArticleForUpdateAsync(Guid articleId)
    {
        var article = await _unitOfWork.GetRepository<Article>().GetAsync(a => a.Id == articleId && !a.IsDeleted);

        if (article != null)
        {
            var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync(c => !c.IsDeleted);

            var articleUpdateDto = _mapper.Map<ArticleUpdateDto>(article);
            articleUpdateDto.Categories = _mapper.Map<List<CategoryDto>>(categories);
            return new DataResult<ArticleUpdateDto>(ResultStatus.Success, articleUpdateDto);
        }

        return new DataResult<ArticleUpdateDto>(ResultStatus.Error, null);
    }

    public async Task<IDataResult<ArticleDto>> GetArticleWithCategoryNonDeletedAsync(Guid articleId)
    {
        //var articleDto = _mapper.Map<ArticleDto>(await GetArticleAsync(articleId));
        var dataResult = await GetArticleAsync(articleId);

        if (dataResult.ResultStatus == ResultStatus.Success)
        {
            var articleDto = _mapper.Map<ArticleDto>(dataResult.Data);
            return new DataResult<ArticleDto>(ResultStatus.Success, articleDto);
        }

        return new DataResult<ArticleDto>(ResultStatus.Error, "", null);
    }

    public async Task<IResult> SafeDeleteArticleAsync(Guid articleId)
    {
        var dataResult = await GetArticleAsync(articleId);

        if (dataResult.Data != null)
        {
            await _unitOfWork.GetRepository<Article>().DeleteAsync(dataResult.Data);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.Article.Delete(dataResult.Data.Title));
        }

        return new Result(ResultStatus.Error, "");
    }

    public async Task<IDataResult<ArticleUpdateDto>> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto)
    {
        var dataResult = await GetArticleAsync(articleUpdateDto.Id);

        if (dataResult.Data == null)
            return new DataResult<ArticleUpdateDto>(ResultStatus.Error, null);

        _mapper.Map(articleUpdateDto, dataResult.Data);
        await _unitOfWork.GetRepository<Article>().UpdateAsync(dataResult.Data);
        await _unitOfWork.SaveAsync();
        return new DataResult<ArticleUpdateDto>(ResultStatus.Success, Messages.Article.Update(articleUpdateDto.Title), articleUpdateDto);
    }


    // Get
    private async Task<IDataResult<Article>> GetArticleAsync(Guid articleId)
    {
        var article = await _unitOfWork.GetRepository<Article>().GetAsync(a => !a.IsDeleted && a.Id == articleId, a => a.Category);

        if (article != null)
            return new DataResult<Article>(ResultStatus.Success, article);

        return new DataResult<Article>(ResultStatus.Error, null);
    }
}