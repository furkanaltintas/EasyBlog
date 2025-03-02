using EasyBlog.Entity.DTOs.Categories;

namespace EasyBlog.Service.Services.Abstractions;

public interface ICategoryService
{
    Task<IList<CategoryDto>> GetAllCategoriesNonDeletedAsync();
}