using AutoMapper;
using EasyBlog.Core.Enums;
using EasyBlog.Core.Utilities.Results.Abstract;
using EasyBlog.Core.Utilities.Results.ComplexTypes;
using EasyBlog.Core.Utilities.Results.Concrete;
using EasyBlog.Entity.DTOs.Users;
using EasyBlog.Entity.Entities;
using EasyBlog.Service.Extensions;
using EasyBlog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EasyBlog.Service.Services.Concretes;

public class UserProfileService : IUserProfileService
{
    private readonly IMapper _mapper;
    private readonly ClaimsPrincipal _user;
    private readonly IImageService _imageService;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;

    public UserProfileService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IImageService imageService, Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _mapper = mapper;
        _userManager = userManager;
        _imageService = imageService;
        _signInManager = signInManager;
        _httpContextAccessor = httpContextAccessor;
        _user = _httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
    }


    // Profil getirme
    public async Task<IDataResult<UserProfileDto>> GetUserAsync()
    {
        var user = await _userManager
            .Users.Include(u => u.Image)
            .FirstOrDefaultAsync(u => u.Id == _user.GetLoggedInUserId());

        if (user == null)
            return new DataResult<UserProfileDto>(ResultStatus.Error, "Sistemde böyle bir kullanıcı bulunmamaktadır.");

        var userProfileDto = _mapper.Map<UserProfileDto>(user);
        return new DataResult<UserProfileDto>(ResultStatus.Success, userProfileDto);
    }


    // Profil sorgulama
    public async Task<IResult> ChangeUserAsync(UserProfileDto userProfileDto)
    {
        var user = await _userManager.Users
            .Include(u => u.Image) // Image dahil edildi
            .FirstOrDefaultAsync(u => u.Id == _user.GetLoggedInUserId());

        if (user == null)
            return new Result(ResultStatus.Error, "Sistemde böyle bir kullanıcı bulunmamaktadır.");

        var isVerified = await _userManager.CheckPasswordAsync(user, userProfileDto.CurrentPassword);
        if (!isVerified)
            return new Result(ResultStatus.Error, "Mevcut şifre hatalı.");

        bool passwordChanged = false;

        // Fotoğraf güncelleme
        var imageUploadResult = await UploadUserProfileImageAsync(userProfileDto);
        if (imageUploadResult != Guid.Empty)
        {
            user.ImageId = imageUploadResult;
            _imageService.Delete(user.Image.FileName);
        }

        // Şifre değiştirme işlemi
        if (userProfileDto.NewPassword != null)
        {
            var passwordChangeResult = await ChangePasswordAsync(user, userProfileDto.CurrentPassword, userProfileDto.NewPassword);
            if (passwordChangeResult.ResultStatus == ResultStatus.Error)
                return passwordChangeResult;

            passwordChanged = true; // Şifre başarılı şekilde değiştirildi.
        }

        // Profil güncelleme işlemi
        var updateResult = await UpdateUserProfileAsync(user, userProfileDto);
        if (updateResult.ResultStatus == ResultStatus.Error)
            return updateResult;

        // Şifre değiştirildiyse farklı mesaj döndür
        string successMessage = passwordChanged ? "Değişiklikler güncellendi. Parolanız başarıyla değiştirildi." : "Değişiklikler güncellendi.";
        return new Result(ResultStatus.Success, successMessage);
    }


    // Şifre değiştirme işlemi
    private async Task<IResult> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword)
    {
        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        if (!result.Succeeded)
            return new Result(ResultStatus.Error, "Şifreniz değiştirilemedi.");

        await _userManager.UpdateSecurityStampAsync(user);
        await _signInManager.SignOutAsync();
        await _signInManager.PasswordSignInAsync(user, newPassword, true, false);
        return new Result(ResultStatus.Success, "Parola başarıyla değiştirildi.");
    }


    // Profil güncelleme işlemi
    private async Task<IResult> UpdateUserProfileAsync(AppUser user, UserProfileDto userProfileDto)
    {
        var existingImageId = user.ImageId;
        _mapper.Map(userProfileDto, user);
        user.ImageId ??= existingImageId; // Eğer ImageId null olduysa, eski değeri geri ata
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return new Result(ResultStatus.Error, "Kullanıcı bilgileri güncellenirken bir hata oluştu.");

        return new Result(ResultStatus.Success, "Profil bilgileri başarıyla güncellendi.");
    }


    // Fotoğraf güncelleme
    private async Task<Guid> UploadUserProfileImageAsync(UserProfileDto userProfileDto)
    {
        return userProfileDto.Photo != null
            ? await _imageService.PhotoUploadAsync($"{userProfileDto.FirstName}{userProfileDto.LastName}", userProfileDto.Photo, ImageType.User)
            : Guid.Empty;
    }
}
