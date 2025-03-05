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

    Task<IDataResult<ArticleDto>> GetArticleAsync(Guid articleId);

    IDataResult<ArticlePaginationDto> GetAllByPaging(Guid? categoryId, int currentPage = 1, int pageSize = 3, bool isAscending = false);
    IDataResult<ArticlePaginationDto> Search(string keyword, int currentPage = 1, int pageSize = 3, bool isAscending = false);
}