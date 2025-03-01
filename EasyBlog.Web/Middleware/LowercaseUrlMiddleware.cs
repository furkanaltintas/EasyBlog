namespace EasyBlog.Web.Middleware;

public class LowercaseUrlMiddleware
{
    private readonly RequestDelegate _next;

    public LowercaseUrlMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var request = context.Request;
        var path = request.Path.Value;

        if (!string.IsNullOrEmpty(path) && path != path.ToLower())
        {
            var lowerCaseUrl = request.Scheme + "://" + request.Host + request.PathBase + path.ToLower() + request.QueryString;
            context.Response.Redirect(lowerCaseUrl, true); // 301 yönlendirme
            return;
        }

        await _next(context);
    }
}