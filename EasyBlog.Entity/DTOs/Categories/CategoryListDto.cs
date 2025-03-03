using EasyBlog.Core.Entities.Abstract;

namespace EasyBlog.Entity.DTOs.Categories;

public class CategoryListDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsDeleted { get; set; }
}