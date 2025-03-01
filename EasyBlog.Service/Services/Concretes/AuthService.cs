using EasyBlog.Entity.DTOs.Users;
using EasyBlog.Entity.Entities;
using EasyBlog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace EasyBlog.Service.Services.Concretes;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<AuthResultDto> LoginAsync(UserLoginDto userLoginDto)
    {
        var user = await _userManager.FindByEmailAsync(userLoginDto.Email);

        if (user == null)
            return new AuthResultDto { Success = false, Message = "E-posta adresiniz veya şifreniz yanlıştır" };

        var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, false);

        if (!result.Succeeded)
            return new AuthResultDto { Success = false, Message = "E-posta adresiniz veya şifreniz yanlıştır" };

        return new AuthResultDto { Success = true };
    }


    public async Task Logout() => await _signInManager.SignOutAsync();
}