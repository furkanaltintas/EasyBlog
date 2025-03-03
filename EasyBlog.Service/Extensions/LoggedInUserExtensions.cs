using System.Security.Claims;

namespace EasyBlog.Service.Extensions;

public static class LoggedInUserExtensions
{
    public static Guid GetLoggedInUserId(this ClaimsPrincipal principal) => Guid.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier));

    public static string GetLoggedInEmail(this ClaimsPrincipal principal) => principal.FindFirstValue(ClaimTypes.Email);
}