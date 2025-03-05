using EasyBlog.Core.Utilities.Results.ComplexTypes;
using EasyBlog.Service.Services.Managers;
using EasyBlog.Web.Areas.Management.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyBlog.Web.Controllers;

public class HomeController : Controller
{
    private IBaseService _serviceManager;

    public HomeController(IBaseService serviceManager)
    {
        _serviceManager = serviceManager;
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
        var result = await _serviceManager.ArticleService.GetArticleAsync(articleId);

        if (result.ResultStatus == ResultStatus.Success) return View(result.Data);
        return RedirectToAction(nameof(Index));
    }
}
