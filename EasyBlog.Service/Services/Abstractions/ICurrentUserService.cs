namespace EasyBlog.Service.Services.Abstractions;

public interface ICurrentUserService
{
    Guid GetCurrentUserId();
}