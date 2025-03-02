using EasyBlog.Service.Services.Abstractions;

namespace EasyBlog.Service.Services.Managers;

public interface IBaseService
{
    IAuthService AuthService { get; }
    IArticleService ArticleService { get; }
    ICategoryService CategoryService { get; }
}