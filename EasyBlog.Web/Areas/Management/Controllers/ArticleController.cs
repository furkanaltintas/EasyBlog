using EasyBlog.Core.Utilities.Results.ComplexTypes;
using EasyBlog.Entity.DTOs.Articles;
using EasyBlog.Entity.DTOs.Categories;
using EasyBlog.Service.Services.Managers;
using EasyBlog.Web.Constants;
using EasyBlog.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace EasyBlog.Web.Areas.Management.Controllers;

[Authorize]
[Route("yonetim")]
public class ArticleController : BaseController
{
    private readonly IToastNotification _toastNotification;

    public ArticleController(IBaseService serviceManager, IToastNotification toastNotification) : base(serviceManager)
    {
        _toastNotification = toastNotification;
    }


    [Route(RouteConstants.Article)]
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
        if (ModelState.IsValid)
        {
            var result = await _serviceManager.ArticleService.CreateArticleAsync(articleAddDto);

            if (result.ResultStatus == ResultStatus.Success)
            {
                _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions { Title = "Başarılı İşlem!" });
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, result.Message);
        }


        ToastHelper.AddErrorsToToastNotification(ModelState, _toastNotification);
        articleAddDto.Categories = await _serviceManager.CategoryService.GetAllCategoriesNonDeletedAsync();
        return View(articleAddDto);
    }


    [Route(RouteConstants.Update + "/{articleId:guid}")]
    public async Task<IActionResult> Update(Guid articleId)
    {
        var dataResult = await _serviceManager.ArticleService.GetArticleForUpdateAsync(articleId);

        if (dataResult.ResultStatus == ResultStatus.Success)
            return View(dataResult.Data);
        else
            return NotFound();
    }


    [HttpPost(RouteConstants.Update + "/{articleId:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(ArticleUpdateDto articleUpdateDto, Guid articleId)
    {
        if (ModelState.IsValid)
        {
            articleUpdateDto.Id = articleId;
            var dataResult = await _serviceManager.ArticleService.UpdateArticleAsync(articleUpdateDto);

            if (dataResult.ResultStatus == ResultStatus.Success)
            {
                _toastNotification.AddSuccessToastMessage(dataResult.Message, new ToastrOptions() { Title = "Başarılı İşlem" });
                return RedirectToAction(nameof(Index));
            }

        }

        ToastHelper.AddErrorsToToastNotification(ModelState, _toastNotification);
        articleUpdateDto.Categories = await GetCategoriesAsync();
        return View(articleUpdateDto);
    }


    [Route(RouteConstants.Delete)]
    public async Task<IActionResult> Delete(Guid articleId)
    {
        var result = await _serviceManager.ArticleService.SafeDeleteArticleAsync(articleId);

        if (result.ResultStatus == ResultStatus.Success)
        {
            _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions() { Title = "İşlem Başarılı" });
            return RedirectToAction(nameof(Index));
        }
        else
            return NotFound();
    }



    #region Category SelectList
    private async Task<IList<CategoryDto>> GetCategoriesAsync() => await _serviceManager.CategoryService.GetAllCategoriesNonDeletedAsync();
    #endregion
}