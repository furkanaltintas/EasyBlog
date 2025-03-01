using EasyBlog.Entity.DTOs.Users;
using EasyBlog.Entity.Entities;
using EasyBlog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyBlog.Web.Areas.Management.Controllers;

[Area("Management")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
        if (!ModelState.IsValid)
            return View();

        var result = await _authService.LoginAsync(userLoginDto);

        if (result.Success)
            return RedirectToAction("Index", "Home", new { Area = "Management" });

        ModelState.AddModelError(string.Empty, result.Message);
        return View();
    }


    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _authService.Logout();
        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Home", new { Area = "" });
    }
}