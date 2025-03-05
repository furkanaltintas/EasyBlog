using EasyBlog.Service.Services.Abstractions;

namespace EasyBlog.Service.Services.Managers;

public class BaseService : IBaseService
{
    public BaseService(
        IUserService userService,
        IRoleService roleService,
        IAuthService authService,
        IImageService imageService,
        IArticleService articleService,
        ICategoryService categoryService,
        IDashboardService dashboardService,
        IUserProfileService userProfileService,
        IUserRegistrationService userRegistrationService)
    {
        ImageService = imageService;
        UserService = userService;
        RoleService = roleService;
        AuthService = authService;
        ArticleService = articleService;
        CategoryService = categoryService;
        DashboardService = dashboardService;
    }

    public IUserService UserService { get; }
    public IRoleService RoleService { get; }
    public IAuthService AuthService { get; }
    public IImageService ImageService { get; }
    public IArticleService ArticleService { get; }
    public ICategoryService CategoryService { get; }
    public IDashboardService DashboardService { get; }
    public IUserProfileService UserProfileService { get; }
    public IUserRegistrationService UserRegistrationService { get; }   
}