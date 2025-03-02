using EasyBlog.Core.Entities;
using EasyBlog.Entity.DTOs.Categories;

namespace EasyBlog.Entity.DTOs.Articles;

public class ArticleListDto : IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public CategoryDto Category { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public bool IsDeleted { get; set; }

}