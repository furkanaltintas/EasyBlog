using EasyBlog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyBlog.Web.Areas.Management.Controllers;

[Area("Management")]
[Authorize]
public class HomeController : Controller
{
    private readonly IArticleService _articleService;

    public HomeController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    public async Task<IActionResult> Index()
    {
        var articles = await _articleService.GetAllArticlesAsync();
        return View(articles);
    }
}