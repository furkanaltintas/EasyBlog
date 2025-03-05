using EasyBlog.Service.Services.Abstractions;

namespace EasyBlog.Service.Services.Managers;

public interface IBaseService
{
    IUserService UserService { get; }
    IRoleService RoleService { get; }
    IAuthService AuthService { get; }
    IImageService ImageService { get; }
    IArticleService ArticleService { get; }
    ICategoryService CategoryService { get; }
    IDashboardService DashboardService { get; }
    IUserProfileService UserProfileService { get; }
    IUserRegistrationService UserRegistrationService { get; }
}