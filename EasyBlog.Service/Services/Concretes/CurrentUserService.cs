using EasyBlog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EasyBlog.Service.Services.Concretes;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor) { _httpContextAccessor = httpContextAccessor; }

    public Guid GetCurrentUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(userIdClaim, out var userId) ? userId : Guid.Empty;
    }
}