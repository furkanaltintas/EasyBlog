using EasyBlog.Entity.DTOs.Articles;

namespace EasyBlog.Service.Services.Abstractions;

public interface IArticleService
{
    Task SafeDeleteArticleAsync(Guid articleId);

    Task CreateArticleAsync(ArticleAddDto articleAddDto);

    Task<ArticleUpdateDto> GetArticleForUpdateAsync(Guid articleId);

    Task<bool> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto);


    Task<ArticleDto> GetArticleWithCategoryNonDeletedAsync(Guid articleId);

    Task<List<ArticleListDto>> GetAllArticlesWithCategoryNonDeletedAsync();
}