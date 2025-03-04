using EasyBlog.Core.Utilities.Results.ComplexTypes;
using EasyBlog.Entity.DTOs.Articles;
using EasyBlog.Entity.DTOs.Categories;
using EasyBlog.Service.Services.Managers;
using EasyBlog.Web.Areas.Management.Controllers.Base;
using EasyBlog.Web.Constants;
using EasyBlog.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace EasyBlog.Web.Areas.Management.Controllers;

[Authorize]
[Route(RouteConstants.Article)]
public class ArticleController : BaseController
{
    private readonly IToastNotification _toastNotification;

    public ArticleController(IToastNotification toastNotification, IBaseService baseService) : base(baseService)
    {
        _toastNotification = toastNotification;
    }


    [Authorize(Roles = $"{RoleConsts.SuperAdmin}, {RoleConsts.Admin}, {RoleConsts.User}")]
    public async Task<IActionResult> Index()
    {
        var result = await _serviceManager.ArticleService.GetAllArticlesWithCategoryNonDeletedAsync();
        if (result.ResultStatus == ResultStatus.Success) return View(result.Data);
        else return View(result.Data);
    }


    [Authorize(Roles = $"{RoleConsts.SuperAdmin}, {RoleConsts.Admin}")]
    [Route(RouteConstants.DeletedArticles)]
    public async Task<IActionResult> DeletedArticle()
    {
        var result = await _serviceManager.ArticleService.GetAllArticlesWithCategoryDeletedAsync();
        if (result.ResultStatus == ResultStatus.Success) return View(result.Data);
        else return View(result.Data);
    }


    [Authorize(Roles = $"{RoleConsts.SuperAdmin}, {RoleConsts.Admin}")]
    [Route(RouteConstants.Add)]
    public async Task<IActionResult> Add()
    {
        var articleAddDto = new ArticleAddDto { Categories = await GetCategoriesAsync() };
        return View(articleAddDto);
    }


    [Authorize(Roles = $"{RoleConsts.SuperAdmin}, {RoleConsts.Admin}")]
    [HttpPost(RouteConstants.Add)]
    public async Task<IActionResult> Add(ArticleAddDto articleAddDto)
    {
        var result = await _serviceManager.ArticleService.CreateArticleAsync(articleAddDto);

        if (result.ResultStatus == ResultStatus.Success)
        {
            NotificationHelper.ShowSuccess(_toastNotification, result.Message);
            return RedirectToAction(nameof(Index));
        }

        NotificationHelper.ShowError(_toastNotification, result.Message);
        var dataResult = await _serviceManager.CategoryService.GetAllCategoriesNonDeletedAsync();
        articleAddDto.Categories = dataResult.Data;
        return View(articleAddDto);
    }


    [Authorize(Roles = $"{RoleConsts.SuperAdmin}, {RoleConsts.Admin}")]
    [Route(RouteConstants.Update + "/{articleId:guid}")]
    public async Task<IActionResult> Update(Guid articleId)
    {
        var dataResult = await _serviceManager.ArticleService.GetArticleForUpdateAsync(articleId);

        if (dataResult.ResultStatus == ResultStatus.Success)
            return View(dataResult.Data);
        else
            NotificationHelper.ShowError(_toastNotification, dataResult.Message);
        return RedirectToAction(nameof(Index));
    }


    [Authorize(Roles = $"{RoleConsts.SuperAdmin}, {RoleConsts.Admin}")]
    [HttpPost(RouteConstants.Update + "/{articleId:guid}")]
    public async Task<IActionResult> Update(ArticleUpdateDto articleUpdateDto, Guid articleId)
    {
        articleUpdateDto.Id = articleId;
        var dataResult = await _serviceManager.ArticleService.UpdateArticleAsync(articleUpdateDto);

        if (dataResult.ResultStatus == ResultStatus.Success)
        {
            NotificationHelper.ShowSuccess(_toastNotification, dataResult.Message);
            return RedirectToAction(nameof(Index));
        }

        NotificationHelper.ShowSuccess(_toastNotification, dataResult.Message);
        articleUpdateDto.Categories = await GetCategoriesAsync();
        return View(articleUpdateDto);
    }


    [Authorize(Roles = $"{RoleConsts.SuperAdmin}, {RoleConsts.Admin}")]
    [Route(RouteConstants.Delete)]
    public async Task<IActionResult> Delete(Guid articleId)
    {
        var result = await  _serviceManager.ArticleService.SafeDeleteArticleAsync(articleId);

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
    public async Task<IActionResult> UndoDelete(Guid articleId)
    {
        var result = await _serviceManager.ArticleService.UndoDeleteArticleAsync(articleId);

        if (result.ResultStatus == ResultStatus.Success)
        {
            NotificationHelper.ShowSuccess(_toastNotification, result.Message);
            return RedirectToAction(nameof(Index));
        }

        NotificationHelper.ShowError(_toastNotification, result.Message);
        return RedirectToAction(nameof(Index));
    }





    #region Category SelectList
    private async Task<IList<CategoryListDto>> GetCategoriesAsync()
    {
        var dataResult = await _serviceManager.CategoryService.GetAllCategoriesNonDeletedAsync();
        return dataResult.Data;
    }
    #endregion
}