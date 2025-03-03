using EasyBlog.Service.Services.Abstractions;

namespace EasyBlog.Service.Services.Managers;

public class BaseService : IBaseService
{
    public BaseService(
        IAuthService authService,
        IArticleService articleService,
        ICategoryService categoryService,
        IUserService userService,
        IRoleService roleService)
    {
        AuthService = authService;
        ArticleService = articleService;
        CategoryService = categoryService;
        UserService = userService;
        RoleService = roleService;
    }

    public IAuthService AuthService { get; }

    public IArticleService ArticleService { get; }

    public ICategoryService CategoryService { get; }

    public IUserService UserService { get; }

    public IRoleService RoleService { get; }
}
