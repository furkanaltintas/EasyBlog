﻿using EasyBlog.Core.Utilities.Results.ComplexTypes;
using EasyBlog.Entity.DTOs.Categories;
using EasyBlog.Service.Services.Managers;
using EasyBlog.Web.Areas.Management.Controllers.Base;
using EasyBlog.Web.Constants;
using EasyBlog.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace EasyBlog.Web.Areas.Management.Controllers;

[Route(RouteConstants.Category)]
public class CategoryController : BaseController
{
    private readonly IToastNotification _toastNotification;
    public CategoryController(IToastNotification toastNotification, IBaseService baseService):base(baseService)
    {
        _toastNotification = toastNotification;
    }


    [Authorize(Roles = $"{RoleConsts.SuperAdmin}, {RoleConsts.Admin}, {RoleConsts.User}")]
    public async Task<IActionResult> Index()
    {
        var dataResult = await _serviceManager.CategoryService.GetAllCategoriesNonDeletedAsync();

        return View(dataResult.Data);
    }


    [Authorize(Roles = $"{RoleConsts.SuperAdmin}, {RoleConsts.Admin}")]
    [Route(RouteConstants.DeletedCategories)]
    public async Task<IActionResult> DeletedCategories()
    {
        var dataResult = await _serviceManager.CategoryService.GetAllCategoriesDeletedAsync();

        return View(dataResult.Data);
    }


    [Authorize(Roles = $"{RoleConsts.SuperAdmin}, {RoleConsts.Admin}")]
    [Route(RouteConstants.Add)]
    public IActionResult Add()
    {
        return View();
    }


    [Authorize(Roles = $"{RoleConsts.SuperAdmin}, {RoleConsts.Admin}")]
    [HttpPost(RouteConstants.Add)]
    public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
    {
        var result = await _serviceManager.CategoryService.CreateCategoryAsync(categoryAddDto);

        if (result.ResultStatus == ResultStatus.Success)
        {
            NotificationHelper.ShowSuccess(_toastNotification, result.Message);
            return RedirectToAction(nameof(Index));
        }

        NotificationHelper.ShowError(_toastNotification, result.Message);
        return View(categoryAddDto);
    }


    [Authorize(Roles = $"{RoleConsts.SuperAdmin}, {RoleConsts.Admin}")]
    [HttpPost]
    public async Task<IActionResult> AddWithAjax([FromBody] CategoryAddDto categoryAddDto)
    {
        var result = await _serviceManager.CategoryService.CreateCategoryAsync(categoryAddDto);

        if (result.ResultStatus == ResultStatus.Success)
        {
            NotificationHelper.ShowSuccess(_toastNotification, result.Message);
            return Json(result.Message);
        }

        NotificationHelper.ShowError(_toastNotification, result.Message);
        return Json(result.Message);
    }


    [Authorize(Roles = $"{RoleConsts.SuperAdmin}, {RoleConsts.Admin}")]
    [Route(RouteConstants.Update + "/{categoryId:guid}")]
    public async Task<IActionResult> Update(Guid categoryId)
    {
        var dataResult = await _serviceManager.CategoryService.GetCategoryByUpdateGuidAsync(categoryId);

        if (dataResult.ResultStatus == ResultStatus.Success)
            return View(dataResult.Data);

        NotificationHelper.ShowError(_toastNotification, dataResult.Message);
        return RedirectToAction(nameof(Index));
    }


    [Authorize(Roles = $"{RoleConsts.SuperAdmin}, {RoleConsts.Admin}")]
    [HttpPost(RouteConstants.Update + "/{categoryId:guid}")]
    public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto, Guid categoryId)
    {
        var dataResult = await _serviceManager.CategoryService.UpdateCategoryAsync(categoryUpdateDto, categoryId);

        if (dataResult.ResultStatus == ResultStatus.Success)
        {
            NotificationHelper.ShowSuccess(_toastNotification, dataResult.Message);
            return RedirectToAction(nameof(Index));
        }

        NotificationHelper.ShowError(_toastNotification, dataResult.Message);
        return View(categoryUpdateDto);
    }


    [Authorize(Roles = $"{RoleConsts.SuperAdmin}, {RoleConsts.Admin}")]
    [Route(RouteConstants.Delete)]
    public async Task<IActionResult> Delete(Guid categoryId)
    {
        var result = await _serviceManager.CategoryService.SafeDeleteCategoryAsync(categoryId);

        if (result.ResultStatus == ResultStatus.Success)
        {
            NotificationHelper.ShowSuccess(_toastNotification, result.Message);
            return RedirectToAction(nameof(Index));
        }

        NotificationHelper.ShowError(_toastNotification, result.Message);
        return RedirectToAction(nameof(Index));
    }


    [Authorize(Roles = $"{RoleConsts.SuperAdmin}, {RoleConsts.Admin}")]
    [Route(RouteConstants.UndoDelete)]
    public async Task<IActionResult> UndoDelete(Guid categoryId)
    {
        var result = await _serviceManager.CategoryService.UndoDeleteCategoryAsync(categoryId);

        if (result.ResultStatus == ResultStatus.Success)
        {
            NotificationHelper.ShowSuccess(_toastNotification, result.Message);
            return RedirectToAction(nameof(Index));
        }

        NotificationHelper.ShowError(_toastNotification, result.Message);
        return RedirectToAction(nameof(Index));
    }
}