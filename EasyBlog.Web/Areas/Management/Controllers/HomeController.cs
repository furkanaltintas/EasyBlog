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
    public IActionResult Index() => View();

    [HttpGet("yearlyarticlecounts")]
    public async Task<IActionResult> YearlyArticleCounts() => Json(JsonConvert.SerializeObject(await _serviceManager.DashboardService.GetYearlyArticleCounts()));

    [HttpGet("totalarticlecount")]
    public async Task<IActionResult> TotalArticleCount() => Json(await _serviceManager.DashboardService.GetTotalArticleCount());

    [HttpGet("totalcategorycount")]
    public async Task<IActionResult> TotalCategoryCount() => Json(await _serviceManager.DashboardService.GetTotalCategoryCount());

    [HttpGet("totalusercount")]
    public async Task<IActionResult> TotalUserCount() => Json(await _serviceManager.DashboardService.GetTotalUserCount());
}