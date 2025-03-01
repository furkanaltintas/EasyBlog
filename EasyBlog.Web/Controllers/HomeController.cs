using EasyBlog.Service.Services.Abstractions;
using EasyBlog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
        var articles = await _articleService.GetAllArticlesAsync();
        return View(articles);
    }

    public async Task<IActionResult> Privacy()
    {
        return View();
    }
}
