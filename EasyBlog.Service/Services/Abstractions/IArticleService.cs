using EasyBlog.Entity.DTOs.Articles;

namespace EasyBlog.Service.Services.Abstractions;

public interface IArticleService
{
    Task CreateArticleAsync(ArticleAddDto articleAddDto);

    Task<List<ArticleListDto>> GetAllArticlesWithCategoryNonDeletedAsync();

}