using Microsoft.AspNetCore.Mvc.Rendering;

namespace EasyBlog.Web.Helpers;

public class UrlHelper
{
    public static string IsActive(string controller, ViewContext viewContext, string actionName = null)
    {
        var currentController = viewContext.RouteData.Values["controller"]?.ToString();
        if (actionName == null) return currentController?.ToLower() == controller.ToLower() ? "active" : string.Empty;
        var currentAction = viewContext.RouteData.Values["actionName"]?.ToString();
        return currentController?.ToLower() == controller.ToLower() && currentAction.ToLower() == actionName.ToLower() ? "active" : string.Empty;
    }

    public static string GetSecondSegment(HttpRequest request)
    {
        var segments = request.Path.Value?.Trim('/').Split('/');
        return segments.Length > 1 ? segments[1] : segments[0];
    }
}