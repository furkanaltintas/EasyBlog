using Autofac.Extras.DynamicProxy;
using AutoMapper;
using EasyBlog.Core.Enums;
using EasyBlog.Core.Utilities.Results.Abstract;
using EasyBlog.Core.Utilities.Results.ComplexTypes;
using EasyBlog.Core.Utilities.Results.Concrete;
using EasyBlog.Data.UnitOfWorks;
using EasyBlog.Entity.DTOs.Articles;
using EasyBlog.Entity.DTOs.Categories;
using EasyBlog.Entity.Entities;
using EasyBlog.Service.Aspects;
using EasyBlog.Service.Extensions;
using EasyBlog.Service.Helpers.Images.Abstractions;
using EasyBlog.Service.Services.Abstractions;
using EasyBlog.Service.Services.Managers;
using EasyBlog.Service.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EasyBlog.Service.Services.Concretes;

[Intercept(typeof(ValidationAspect))]
[Intercept(typeof(CacheAspect))]
public class ArticleService : RepositoryService, IArticleService
{
    private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;
    private readonly IImageHelper _imageHelper;
    private readonly ClaimsPrincipal _user;

    public ArticleService(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor,
        IImageHelper imageHelper) : base(mapper, unitOfWork)
    {
        _httpContextAccessor = httpContextAccessor;
        _user = _httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
        _imageHelper = imageHelper;
    }

    public async Task<IResult> CreateArticleAsync(ArticleAddDto articleAddDto)
    {
        var userId = _user.GetLoggedInUserId();

        if (userId == Guid.Empty)
            return new Result(ResultStatus.Error, Messages.Auth.UserInvalid());

        #region Resim ekleme işlemi
        var imageUpload = await _imageHelper.Upload(articleAddDto.Title, articleAddDto.Photo, ImageType.Post);
        Image image = new(imageUpload.FullName, articleAddDto.Photo.ContentType);
        await _unitOfWork.GetRepository<Image>().AddAsync(image);
        #endregion

        var article = _mapper.Map<Article>(articleAddDto);
        article.ImageId = image.Id;
        article.UserId = userId;

        await _unitOfWork.GetRepository<Article>().AddAsync(article);
        await _unitOfWork.SaveAsync();
        return new Result(ResultStatus.Success, Messages.Article.Add(article.Title));
    }

    public async Task<IDataResult<List<ArticleListDto>>> GetAllArticlesWithCategoryNonDeletedAsync()
    {
        var articles = await _unitOfWork
            .GetRepository<Article>()
            .GetAllAsync(a => !a.IsDeleted, a => a.Category);

        if (articles.Any())
        {
            var articleListDtos = _mapper.Map<List<ArticleListDto>>(articles);
            return new DataResult<List<ArticleListDto>>(ResultStatus.Success, articleListDtos);
        }

        return new DataResult<List<ArticleListDto>>(ResultStatus.Error, Messages.Article.NotFound(true), new());
    }

    public async Task<IDataResult<IList<ArticleListDto>>> GetAllArticlesWithCategoryDeletedAsync()
    {
        var articles = await _unitOfWork.GetRepository<Article>().GetAllAsync(a => a.IsDeleted, a => a.Category);
        var articleListDtos = _mapper.Map<List<ArticleListDto>>(articles);

        return new DataResult<List<ArticleListDto>>(ResultStatus.Success, articleListDtos);
    }

    public async Task<IDataResult<ArticleUpdateDto>> GetArticleForUpdateAsync(Guid articleId)
    {
        var dataResult = await GetByArticleAsync(articleId);


        if (dataResult.ResultStatus == ResultStatus.Success)
        {
            var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync(c => !c.IsDeleted);

            var articleUpdateDto = _mapper.Map<ArticleUpdateDto>(dataResult.Data);
            articleUpdateDto.Categories = _mapper.Map<List<CategoryListDto>>(categories);
            return new DataResult<ArticleUpdateDto>(ResultStatus.Success, articleUpdateDto);
        }

        return new DataResult<ArticleUpdateDto>(ResultStatus.Error, Messages.Article.NotFoundById(articleId), null);
    }

    public async Task<IDataResult<ArticleDto>> GetArticleWithCategoryNonDeletedAsync(Guid articleId)
    {
        var dataResult = await GetByArticleAsync(articleId);

        if (dataResult.ResultStatus == ResultStatus.Success)
        {
            var articleDto = _mapper.Map<ArticleDto>(dataResult.Data);
            return new DataResult<ArticleDto>(ResultStatus.Success, articleDto);
        }

        return new DataResult<ArticleDto>(ResultStatus.Error);
    }

    public async Task<IResult> SafeDeleteArticleAsync(Guid articleId)
    {
        var dataResult = await GetByArticleAsync(articleId);

        if (dataResult.Data != null)
        {
            await _unitOfWork.GetRepository<Article>().DeleteAsync(dataResult.Data);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.Article.Delete(dataResult.Data.Title));
        }

        return new Result(ResultStatus.Error, Messages.Article.NotFoundById(articleId));
    }

    public async Task<IResult> UndoDeleteArticleAsync(Guid articleId)
    {
        var article = await _unitOfWork.GetRepository<Article>().GetByGuidAsync(articleId);
        article.IsDeleted = false;
        article.DeletedDate = null;
        article.DeletedBy = null;

        await _unitOfWork.GetRepository<Article>().UpdateAsync(article);
        await _unitOfWork.SaveAsync();

        return new Result(ResultStatus.Success, Messages.Article.UndoDelete(article.Title));
    }

    public async Task<IDataResult<ArticleUpdateDto>> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto)
    {
        var dataResult = await GetByArticleAsync(articleUpdateDto.Id);

        if (dataResult.Data == null)
            return new DataResult<ArticleUpdateDto>(ResultStatus.Error, null);

        if (articleUpdateDto.Photo != null)
        {
            _imageHelper.Delete(dataResult.Data.Image.FileName);

            var imageUpload = await _imageHelper.Upload(articleUpdateDto.Title, articleUpdateDto.Photo, ImageType.Post);
            Image image = new(imageUpload.FullName, articleUpdateDto.Photo.ContentType);
            await _unitOfWork.GetRepository<Image>().AddAsync(image);

            dataResult.Data.ImageId = image.Id;
        }


        // HATA
        _mapper.Map(articleUpdateDto, dataResult.Data);
        await _unitOfWork.GetRepository<Article>().UpdateAsync(dataResult.Data);
        await _unitOfWork.SaveAsync();
        return new DataResult<ArticleUpdateDto>(ResultStatus.Success, Messages.Article.Update(articleUpdateDto.Title), articleUpdateDto);
    }

    public IDataResult<ArticlePaginationDto> GetAllByPaging(Guid? categoryId, int currentPage = 1, int pageSize = 3, bool isAscending = false)
    {
        pageSize = pageSize > 20 ? 20 : pageSize;

        var query = GetArticlesQuery(categoryId, null);
        var sortedArticles = SortArticles(query, isAscending).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        var totalCount = query.Count();

        var articlePaginationDto = new ArticlePaginationDto
        {
            Articles = sortedArticles,
            CategoryId = categoryId,
            CurrentPage = currentPage,
            PageSize = pageSize,
            TotalCount = totalCount,
            IsAscending = isAscending
        };

        return new DataResult<ArticlePaginationDto>(ResultStatus.Success, articlePaginationDto);
    }

    public IDataResult<ArticlePaginationDto> Search(string keyword, int currentPage = 1, int pageSize = 3, bool isAscending = false)
    {
        pageSize = pageSize > 20 ? 20 : pageSize;

        var query = GetArticlesQuery(null, keyword);
        var sortedArticles = SortArticles(query, isAscending).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

        var totalCount = query.Count();

        var articlePaginationDto = new ArticlePaginationDto
        {
            Articles = sortedArticles,
            CurrentPage = currentPage,
            PageSize = pageSize,
            TotalCount = totalCount,
            IsAscending = isAscending
        };

        return new DataResult<ArticlePaginationDto>(ResultStatus.Success, articlePaginationDto);
    }

    public async Task<IDataResult<ArticleDto>> GetArticleAsync(Guid articleId)
    {
        var query = _unitOfWork.GetRepository<Article>().Query();
        var article = await query
            .Where(a => !a.IsDeleted && a.Id == articleId)
            .Include(a => a.Category)
            .Include(a => a.User)
            .Include(u => u.Image).FirstOrDefaultAsync();

        if (article == null)
            return new DataResult<ArticleDto>(ResultStatus.Error, Messages.Article.NotFound(false));

        var articleDto = _mapper.Map<ArticleDto>(article);
        return new DataResult<ArticleDto>(ResultStatus.Success, articleDto);


    }





    private IQueryable<Article> GetArticlesQuery(Guid? categoryId, string keyword = null)
    {
        var query = _unitOfWork.GetRepository<Article>().Query().Where(a => !a.IsDeleted);

        if (categoryId.HasValue)
            query = query.Where(a => a.CategoryId == categoryId);

        if (!string.IsNullOrEmpty(keyword))
            query = query.Where(a => a.Title.Contains(keyword) || a.Content.Contains(keyword) || a.Category.Name.Contains(keyword));

        return query.Include(a => a.Category).Include(a => a.Image).Include(a => a.User);
    }


    private IQueryable<Article> SortArticles(IQueryable<Article> articles, bool isAscending, int currentPage = 1, int pageSize = 3)
    {
        return isAscending ?
            articles.OrderBy(a => a.CreatedDate) :
            articles.OrderByDescending(a => a.CreatedDate);
    }


    // Get
    private async Task<IDataResult<Article>> GetByArticleAsync(Guid articleId)
    {
        var article = await _unitOfWork.GetRepository<Article>().GetAsync(a => !a.IsDeleted && a.Id == articleId, a => a.Category, a => a.Image);

        if (article != null)
            return new DataResult<Article>(ResultStatus.Success, article);

        return new DataResult<Article>(ResultStatus.Error, Messages.Article.NotFoundById(articleId));
    }
}