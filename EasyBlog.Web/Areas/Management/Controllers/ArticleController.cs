using EasyBlog.Core.Entities.Abstract;
using EasyBlog.Core.Enums;
using EasyBlog.Core.Utilities.Results.ComplexTypes;
using EasyBlog.Entity.DTOs.Articles;
using EasyBlog.Entity.DTOs.Categories;
using EasyBlog.Service.Extensions;
using EasyBlog.Service.Services.Managers;
using EasyBlog.Web.ResultMessages;
using FluentValidation;
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


    [Route("makaleler")]
    public async Task<IActionResult> Index()
    {
        var result = await _serviceManager.ArticleService.GetAllArticlesWithCategoryNonDeletedAsync();
        if (result.ResultStatus == ResultStatus.Success) return View(result.Data);
        return NotFound();
    }


    [Route("ekleme")]
    public async Task<IActionResult> Add()
    {
        var articleAddDto = (ArticleAddDto)await PrepareArticleAddAndUpdateDtoAsync(TransactionType.Add);
        return View(articleAddDto);
    }


    [HttpPost("ekleme")]
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

        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            _toastNotification.AddErrorToastMessage(error.ErrorMessage);
        articleAddDto.Categories = await _serviceManager.CategoryService.GetAllCategoriesNonDeletedAsync();
        return View(articleAddDto);
    }


    [Route("guncelleme/{articleId:guid}")]
    public async Task<IActionResult> Update(Guid articleId)
    {
        var dataResult = await _serviceManager.ArticleService.GetArticleForUpdateAsync(articleId);

        if (dataResult.ResultStatus == ResultStatus.Success)
            return View(dataResult.Data);
        else
            return NotFound();
    }


    [HttpPost("guncelleme/{articleId:guid}")]
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

        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            _toastNotification.AddErrorToastMessage(error.ErrorMessage);
        return View((ArticleUpdateDto)await PrepareArticleAddAndUpdateDtoAsync(TransactionType.Update, articleUpdateDto));
    }


    [Route("silme")]
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
    private async Task<IList<CategoryDto>> GetCategoriesAsync() =>
    await _serviceManager.CategoryService.GetAllCategoriesNonDeletedAsync();

    private async Task<IDto> PrepareArticleAddAndUpdateDtoAsync(TransactionType type, ArticleUpdateDto? articleUpdateDto = null)
    {
        var categories = await GetCategoriesAsync();

        switch (type)
        {
            case TransactionType.Add:
                return new ArticleAddDto { Categories = categories };
            case TransactionType.Update:
                if (articleUpdateDto == null)
                    throw new ArgumentNullException(nameof(articleUpdateDto), "ArticleUpdateDto cannot be null when updating an article.");

                articleUpdateDto.Categories = categories;
                return articleUpdateDto;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    #endregion
}
