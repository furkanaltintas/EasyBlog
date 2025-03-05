using EasyBlog.Core.Utilities.Results.ComplexTypes;
using EasyBlog.Entity.DTOs.Users;
using EasyBlog.Service.Services.Managers;
using EasyBlog.Web.Areas.Management.Controllers.Base;
using EasyBlog.Web.Constants;
using EasyBlog.Web.Extensions;
using EasyBlog.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace EasyBlog.Web.Areas.Management.Controllers;

[Authorize(Roles = $"{RoleConsts.SuperAdmin}")]
[Route(RouteConstants.User)]
public class UserController : BaseController
{
    private readonly IToastNotification _toastNotification;

    public UserController(
        IBaseService serviceManager,
        IToastNotification toastNotification) : base(serviceManager)
    {
        _toastNotification = toastNotification;
    }


    public async Task<IActionResult> Index()
    {
        var dataResult = await _serviceManager.UserService.GetUsersAsync();
        return this.ResponseView(dataResult, _toastNotification);
    }


    [Route(RouteConstants.Add)]
    public async Task<IActionResult> Add()
    {
        var roles = await _serviceManager.RoleService.GetRolesAsync();
        return View(new UserAddDto { Roles = roles.Data });
    }


    [HttpPost(RouteConstants.Add)]
    public async Task<IActionResult> Add(UserAddDto userAddDto)
    {
        var result = await _serviceManager.UserService.CreateUserAsync(userAddDto);

        if (result.ResultStatus == ResultStatus.Error)
            userAddDto.Roles = (await _serviceManager.RoleService.GetRolesAsync()).Data;

        return this.ResponseRedirectAction(
            userAddDto,
            StatusRedirectHelper.ShowNotification(result),
            result.Message,
            _toastNotification);
    }


    [Route(RouteConstants.Update + "/{userId:guid}")]
    public async Task<IActionResult> Update(Guid userId)
    {
        var dataResult = await _serviceManager.UserService.GetUserByUpdateGuidAsync(userId);

        return this.ResponseView(dataResult, _toastNotification);
    }


    [HttpPost(RouteConstants.Update + "/{userId:guid}")]
    public async Task<IActionResult> Update(UserUpdateDto userUpdateDto, Guid userId)
    {
        var dataResult = await _serviceManager.UserService.UpdateUserAsync(userUpdateDto, userId);

        return this.ResponseRedirectAction(dataResult, _toastNotification, nameof(Index));
    }


    [Route(RouteConstants.Delete + "/{userId:guid}")]
    public async Task<IActionResult> Delete(Guid userId)
    {
        var result = await _serviceManager.UserService.DeleteUserAsync(userId);
        return this.ResponseRedirectAction(result, _toastNotification, nameof(Index));
    }


    [Route(RouteConstants.Profile)]
    public async Task<IActionResult> Profile()
    {
        var dataResult = await _serviceManager.UserService.GetUserAsync();
        return this.ResponseView(dataResult, _toastNotification);
    }


    [HttpPost(RouteConstants.Profile)]
    public async Task<IActionResult> Profile(UserProfileDto userProfileDto)
    {
        var result = await _serviceManager.UserService.ChangeUserAsync(userProfileDto);

        return result.ResultStatus == ResultStatus.Success
            ? RedirectToAction("Index", "Home", new { Area = "Management" })
            : View(userProfileDto);
    }
}