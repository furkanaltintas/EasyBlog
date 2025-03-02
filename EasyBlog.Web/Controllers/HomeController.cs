using EasyBlog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EasyBlog.Web.Controllers;

public class HomeController : Controller
{
    public async Task<IActionResult> Index()
    {
        return View();
    }
}
