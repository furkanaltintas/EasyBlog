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

    public ArticleController(IBaseService serviceManager, IToastNotification toastNotification) : base(serviceManager)
    {
        _toastNotification = toastNotification;
    }


    public async Task<IActionResult> Index()
    {
        var result = await _serviceManager.ArticleService.GetAllArticlesWithCategoryNonDeletedAsync();
        if (result.ResultStatus == ResultStatus.Success) return View(result.Data);
        else return View(result.Data);
    }


    [Route(RouteConstants.Add)]
    public async Task<IActionResult> Add()
    {
        var articleAddDto = new ArticleAddDto { Categories = await GetCategoriesAsync() };
        return View(articleAddDto);
    }


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


    [Route(RouteConstants.Delete)]
    public async Task<IActionResult> Delete(Guid articleId)
    {
        var result = await _serviceManager.ArticleService.SafeDeleteArticleAsync(articleId);

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