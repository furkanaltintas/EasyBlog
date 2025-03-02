using EasyBlog.Core.Entities;
using EasyBlog.Core.Enums;
using EasyBlog.Entity.DTOs.Articles;
using EasyBlog.Service.Services.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult Add() => View(PrepareArticleAddAndUpdateDtoAsync(TransactionType.Add).Result);


    [HttpPost("ekleme")]
    public async Task<IActionResult> Add(ArticleAddDto articleAddDto)
    {
        if (!ModelState.IsValid)
        {
            articleAddDto.Categories = await _serviceManager.CategoryService.GetAllCategoriesNonDeletedAsync();
            return View(articleAddDto);
        }

        await _serviceManager.ArticleService.CreateArticleAsync(articleAddDto);
        return RedirectToAction("Index");
    }

    [Route("guncelleme/{articleId:guid}")]
    public async Task<IActionResult> Update(Guid articleId)
    {
        var articleUpdateDto = await _serviceManager.ArticleService.GetArticleForUpdateAsync(articleId);
        return View(articleUpdateDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("guncelleme/{articleId:guid}")]
    public async Task<IActionResult> Update(ArticleUpdateDto articleUpdateDto, Guid articleId)
    {
        if (!ModelState.IsValid)
        {
            return View(PrepareArticleAddAndUpdateDtoAsync(TransactionType.Update, articleUpdateDto));
        }

        articleUpdateDto.Id = articleId;
        var updateResult = await _serviceManager.ArticleService.UpdateArticleAsync(articleUpdateDto);

        if (updateResult)
        {
            return RedirectToAction("Index");
        }

        ModelState.AddModelError(string.Empty, "Makale güncellenmedi. Lütfen tekrar deneyin.");
        return View(PrepareArticleAddAndUpdateDtoAsync(TransactionType.Update, articleUpdateDto));
    }

    [Route("silme")]
    public IActionResult Delete(Guid articleId)
    {
        return View();
    }


    private async Task<IDto> PrepareArticleAddAndUpdateDtoAsync(TransactionType type, ArticleUpdateDto? articleUpdateDto = null)
    {
        var categories = await _serviceManager.CategoryService.GetAllCategoriesNonDeletedAsync();

        switch (type)
        {
            case TransactionType.Add:
                return new ArticleAddDto
                {
                    Categories = categories
                };
            case TransactionType.Update:
            default:
                if (articleUpdateDto == null)
                    throw new ArgumentNullException(nameof(articleUpdateDto), "ArticleUpdateDto cannot be null when updating an article.");

                articleUpdateDto.Categories = categories;
                return articleUpdateDto;
        }
    }
}
