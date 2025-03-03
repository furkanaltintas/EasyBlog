using EasyBlog.Core.Utilities.Results.ComplexTypes;
using EasyBlog.Entity.DTOs.Categories;
using EasyBlog.Service.Services.Managers;
using EasyBlog.Web.Constants;
using EasyBlog.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace EasyBlog.Web.Areas.Management.Controllers;

[Route(RouteConstants.Category)]
public class CategoryController : BaseController
{
    private readonly IToastNotification _toastNotification;
    public CategoryController(IBaseService serviceManager, IToastNotification toastNotification) : base(serviceManager)
    {
        _toastNotification = toastNotification;
    }


    public async Task<IActionResult> Index()
    {
        var dataResult = await _serviceManager.CategoryService.GetAllCategoriesNonDeletedAsync();

        return View(dataResult.Data);
    }


    [Route(RouteConstants.Add)]
    public IActionResult Add()
    {
        return View();
    }

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


    [Route(RouteConstants.Update + "/{categoryId:guid}")]
    public async Task<IActionResult> Update(Guid categoryId)
    {
        var dataResult = await _serviceManager.CategoryService.GetCategoryByUpdateGuid(categoryId);

        if (dataResult.ResultStatus == ResultStatus.Success)
            return View(dataResult.Data);

        NotificationHelper.ShowError(_toastNotification, dataResult.Message);
        return RedirectToAction(nameof(Index));
    }

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
}