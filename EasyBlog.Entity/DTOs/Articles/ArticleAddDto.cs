using EasyBlog.Core.Entities.Abstract;
using EasyBlog.Entity.DTOs.Categories;
using Microsoft.AspNetCore.Http;

namespace EasyBlog.Entity.DTOs.Articles;

public class ArticleAddDto : IDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid CategoryId { get; set; }

    public IFormFile Photo { get; set; }

    public IList<CategoryListDto> Categories { get; set; } = new List<CategoryListDto>();
}