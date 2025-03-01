using EasyBlog.Entity.DTOs.Users;

namespace EasyBlog.Service.Services.Abstractions;

public interface IAuthService
{
    Task<AuthResultDto> LoginAsync(UserLoginDto userLoginDto);
    Task Logout();
}