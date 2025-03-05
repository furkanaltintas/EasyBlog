using EasyBlog.Core.Utilities.Results.ComplexTypes;
using EasyBlog.Entity.Entities;
using EasyBlog.Service.Services.Managers;
using Microsoft.AspNetCore.Mvc;

namespace EasyBlog.Web.Controllers;

public class HomeController : Controller
{
    private readonly IBaseService _serviceManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HomeController(IBaseService serviceManager, IHttpContextAccessor httpContextAccessor)
    {
        _serviceManager = serviceManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public IActionResult Index(Guid? categoryId, int currentPage = 1, int pageSize = 3, bool isAscending = false, string keyword = null)
    {
        var result = string.IsNullOrEmpty(keyword)
            ? _serviceManager.ArticleService.GetAllByPaging(categoryId, currentPage, pageSize, isAscending)
            : _serviceManager.ArticleService.Search(keyword, currentPage, pageSize, isAscending);

        return View(result.Data);
    }

    public IActionResult Search(string keyword, int currentPage = 1, int pageSize = 3, bool isAscending = false)
    {
        return RedirectToAction(nameof(Index), new
        {
            categoryId = (Guid?)null,
            currentPage,
            pageSize,
            isAscending,
            keyword
        });
    }

    public async Task<IActionResult> Detail(Guid articleId)
    {
        var articleResult = await _serviceManager.ArticleService.GetArticleAsync(articleId);
        await _serviceManager.ArticleVisitorService.CreateArticleVisitorAsync(articleId);

        return View(articleResult.Data);
    }
}