using EasyBlog.Entity.DTOs.Users;
using EasyBlog.Service.Services.Managers;
using EasyBlog.Web.Areas.Management.Controllers.Base;
using EasyBlog.Web.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyBlog.Web.Areas.Management.Controllers;

[Route(RouteConstants.Auth)]
public class AuthController : BaseController
{
    public AuthController(IBaseService serviceManager) : base(serviceManager)
    {
    }



    [HttpGet(RouteConstants.Login)]
    public IActionResult Login() => View();


    [HttpPost(RouteConstants.Login)]
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


    [HttpGet(RouteConstants.Logout)]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _serviceManager.AuthService.Logout();
        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Home", new { Area = "" });
    }


    [HttpGet(RouteConstants.AccessDenied)]
    [Authorize]
    public async Task<IActionResult> AccessDenied()
    {
        return View();
    }
}