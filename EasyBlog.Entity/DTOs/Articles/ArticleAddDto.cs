using EasyBlog.Core.Entities.Abstract;
using EasyBlog.Entity.DTOs.Categories;

namespace EasyBlog.Entity.DTOs.Articles;

public class ArticleAddDto : IDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid CategoryId { get; set; }
    public IList<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
}