using EasyBlog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EasyBlog.Web.Controllers;

public class HomeController : Controller
{
    private readonly IArticleService _articleService;

    public HomeController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    public async Task<IActionResult> Index()
    {
        var articles = await _articleService.GetAllArticlesWithCategoryNonDeletedAsync();
        return View(articles);
    }
}
