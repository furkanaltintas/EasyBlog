using EasyBlog.Entity.DTOs.Users;
using EasyBlog.Service.Services.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyBlog.Web.Areas.Management.Controllers;

public class AuthController : BaseController
{
    public AuthController(IBaseService serviceManager) : base(serviceManager) { }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
        if (!ModelState.IsValid)
            return View();

        var result = await _serviceManager.AuthService.LoginAsync(userLoginDto);

        if (result.Success)
            return RedirectToAction("Index", "Home", new { Area = "Management" });

        ModelState.AddModelError(string.Empty, result.Message);
        return View();
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _serviceManager.AuthService.Logout();
        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Home", new { Area = "" });
    }
}