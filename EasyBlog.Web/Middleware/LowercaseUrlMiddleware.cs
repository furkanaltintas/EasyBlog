using Microsoft.AspNetCore.Http.Extensions;

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
        string path = context.Request.Path.Value!;

        if (!string.IsNullOrEmpty(path) && path != path.ToLower())
        {
            string builder = new UriBuilder(context.Request.GetEncodedUrl())
            {
                Path = path.ToLower(),
                Query = context.Request.QueryString.ToString()
            }.ToString();

            context.Response.Redirect(builder, true);
            return;
        }

        await _next(context);
    }
}