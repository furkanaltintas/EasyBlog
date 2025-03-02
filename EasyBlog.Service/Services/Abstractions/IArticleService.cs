using EasyBlog.Entity.DTOs.Articles;

namespace EasyBlog.Service.Services.Abstractions;

public interface IArticleService
{
    Task<List<ArticleListDto>> GetAllArticlesWithCategoryNonDeletedAsync();
}