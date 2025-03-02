using EasyBlog.Entity.DTOs.Articles;

namespace EasyBlog.Service.Services.Abstractions;

public interface IArticleService
{
    Task CreateArticleAsync(ArticleAddDto articleAddDto);
    Task<bool> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto);

    Task<ArticleUpdateDto> GetArticleForUpdateAsync(Guid articleId);

    Task<ArticleDto> GetArticleWithCategoryNonDeletedAsync(Guid articleId);

    Task<List<ArticleListDto>> GetAllArticlesWithCategoryNonDeletedAsync();
}