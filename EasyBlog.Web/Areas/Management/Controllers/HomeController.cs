using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyBlog.Web.Areas.Management.Controllers;

[Authorize]
[Route("yonetim")]
[Area("Management")]
public class HomeController : Controller
{
    [Route("")]
    public async Task<IActionResult> Index()
    {
        return View();
    }
}