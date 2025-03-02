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
    public async Task<IActionResult> Add() => View(PrepareArticleAddDtoAsync().Result);


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


    private async Task<ArticleAddDto> PrepareArticleAddDtoAsync()
    {
        return new ArticleAddDto
        {
            Categories = await _serviceManager.CategoryService.GetAllCategoriesNonDeletedAsync()
        };
    }
}
