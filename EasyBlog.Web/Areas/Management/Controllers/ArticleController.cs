using EasyBlog.Core.Entities;
using EasyBlog.Core.Enums;
using EasyBlog.Entity.DTOs.Articles;
using EasyBlog.Entity.DTOs.Categories;
using EasyBlog.Service.Services.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyBlog.Web.Areas.Management.Controllers;

[Authorize]
[Route("yonetim")]
public class ArticleController : BaseController
{
    public ArticleController(IServiceManager serviceManager) : base(serviceManager) { }


    [Route("makaleler")]
    public async Task<IActionResult> Index()
    {
        var articles = await _serviceManager.ArticleService.GetAllArticlesWithCategoryNonDeletedAsync();
        return View(articles);
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
        if (!ModelState.IsValid)
        {
            articleAddDto.Categories = await _serviceManager.CategoryService.GetAllCategoriesNonDeletedAsync();
            return View(articleAddDto);
        }

        await _serviceManager.ArticleService.CreateArticleAsync(articleAddDto);
        return RedirectToAction(nameof(Index));
    }


    [Route("guncelleme/{articleId:guid}")]
    public async Task<IActionResult> Update(Guid articleId)
    {
        var articleUpdateDto = await _serviceManager.ArticleService.GetArticleForUpdateAsync(articleId);
        return View(articleUpdateDto);
    }


    [HttpPost("guncelleme/{articleId:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(ArticleUpdateDto articleUpdateDto, Guid articleId)
    {
        if (!ModelState.IsValid)
        {
            return View(PrepareArticleAddAndUpdateDtoAsync(TransactionType.Update, articleUpdateDto));
        }

        articleUpdateDto.Id = articleId;
        var updateResult = await _serviceManager.ArticleService.UpdateArticleAsync(articleUpdateDto);

        if (updateResult)
            return RedirectToAction(nameof(Index));

        ModelState.AddModelError(string.Empty, "Makale güncellenmedi. Lütfen tekrar deneyin.");
        return View(await PrepareArticleAddAndUpdateDtoAsync(TransactionType.Update, articleUpdateDto));
    }


    [Route("silme")]
    public async Task<IActionResult> Delete(Guid articleId)
    {
        await _serviceManager.ArticleService.SafeDeleteArticleAsync(articleId);
        return RedirectToAction(nameof(Index));
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
