using EasyBlog.Entity.Entities;

namespace EasyBlog.Service.Services.Abstractions;

public interface IArticleService
{
    Task<List<Article>> GetAllArticlesAsync();
}