using EasyBlog.Core.Entities.Abstract;
using EasyBlog.Entity.DTOs.Categories;
using EasyBlog.Entity.DTOs.Images;
using EasyBlog.Entity.DTOs.Users;

namespace EasyBlog.Entity.DTOs.Articles;

public class ArticleDto : IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int ViewCount { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }

    public CategoryDto Category { get; set; }
    public ImageDto Image { get; set; }
    public UserDto User { get; set; }
}