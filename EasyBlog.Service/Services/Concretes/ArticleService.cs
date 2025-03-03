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
using EasyBlog.Service.Utilities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using IResult = EasyBlog.Core.Utilities.Results.Abstract.IResult;

namespace EasyBlog.Service.Services.Concretes;

[Intercept(typeof(ValidationAspect))]
[Intercept(typeof(CacheAspect))]
public class ArticleService : RepositoryService, IArticleService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IImageHelper _imageHelper;
    private readonly ClaimsPrincipal _user;

    public ArticleService(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IImageHelper imageHelper,
        IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork)
    {
        _httpContextAccessor = httpContextAccessor;
        _user = httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
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

    public async Task<IDataResult<ArticleUpdateDto>> GetArticleForUpdateAsync(Guid articleId)
    {
        var dataResult = await GetArticleAsync(articleId);


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

        return new Result(ResultStatus.Error, Messages.Article.NotFoundById(articleId));
    }

    public async Task<IDataResult<ArticleUpdateDto>> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto)
    {
        var dataResult = await GetArticleAsync(articleUpdateDto.Id);

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


    // Get
    private async Task<IDataResult<Article>> GetArticleAsync(Guid articleId)
    {
        var article = await _unitOfWork.GetRepository<Article>().GetAsync(a => !a.IsDeleted && a.Id == articleId, a => a.Category, a => a.Image);

        if (article != null)
            return new DataResult<Article>(ResultStatus.Success, article);

        return new DataResult<Article>(ResultStatus.Error, Messages.Article.NotFoundById(articleId), null);
    }
}