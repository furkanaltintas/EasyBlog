using EasyBlog.Core.Entities;

namespace EasyBlog.Entity.DTOs.Categories;

public class CategoryDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}