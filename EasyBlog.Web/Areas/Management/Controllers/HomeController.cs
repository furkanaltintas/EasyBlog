using EasyBlog.Service.Services.Concretes;
using EasyBlog.Service.Services.Managers;
using EasyBlog.Web.Areas.Management.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EasyBlog.Web.Areas.Management.Controllers;

[Authorize]
[Route("yonetim")]
[Area("Management")]
public class HomeController : BaseController
{
    public HomeController(IBaseService serviceManager) : base(serviceManager)
    {
    }

    [Route("v1")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("yearlyarticlecounts")]
    public async Task<IActionResult> YearlyArticleCounts()
    {
        var count = await _serviceManager.DashboardService.GetYearlyArticleCounts();
        return Json(JsonConvert.SerializeObject(count));
    }

    [Route("totalarticlecount")]
    public async Task<IActionResult> TotalArticleCount() => Json(await _serviceManager.DashboardService.GetTotalArticleCount());

    [Route("totalcategorycount")]
    public async Task<IActionResult> TotalCategoryCount() => Json(await _serviceManager.DashboardService.GetTotalCategoryCount());

    [Route("totalusercount")]
    public async Task<IActionResult> TotalUserCount() => Json(await _serviceManager.DashboardService.GetTotalUserCount());
}