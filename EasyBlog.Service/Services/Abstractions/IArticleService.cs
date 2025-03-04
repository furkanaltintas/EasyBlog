using EasyBlog.Core.Utilities.Results.Abstract;
using EasyBlog.Entity.DTOs.Articles;

namespace EasyBlog.Service.Services.Abstractions;

public interface IArticleService
{
    Task<IResult> SafeDeleteArticleAsync(Guid articleId);

    Task<IResult> UndoDeleteArticleAsync(Guid articleId);

    Task<IResult> CreateArticleAsync(ArticleAddDto articleAddDto);

    Task<IDataResult<ArticleUpdateDto>> GetArticleForUpdateAsync(Guid articleId);

    Task<IDataResult<ArticleUpdateDto>> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto);

    Task<IDataResult<ArticleDto>> GetArticleWithCategoryNonDeletedAsync(Guid articleId);

    Task<IDataResult<List<ArticleListDto>>> GetAllArticlesWithCategoryNonDeletedAsync();

    Task<IDataResult<IList<ArticleListDto>>> GetAllArticlesWithCategoryDeletedAsync();
}