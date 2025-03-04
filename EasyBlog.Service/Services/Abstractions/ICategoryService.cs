using EasyBlog.Core.Utilities.Results.Abstract;
using EasyBlog.Entity.DTOs.Categories;

namespace EasyBlog.Service.Services.Abstractions;

public interface ICategoryService
{
    Task<IDataResult<IList<CategoryListDto>>> GetAllCategoriesNonDeletedAsync();
    Task<IDataResult<IList<CategoryListDto>>> GetAllCategoriesDeletedAsync();
    Task<IDataResult<CategoryDto>> GetCategoryByGuid(Guid categoryId);
    Task<IDataResult<CategoryUpdateDto>> GetCategoryByUpdateGuidAsync(Guid categoryId);

    Task<IResult> CreateCategoryAsync(CategoryAddDto categoryAddDto);
    Task<IResult> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto, Guid categoryId);
    Task<IResult> SafeDeleteCategoryAsync(Guid categoryId);
    Task<IResult> UndoDeleteCategoryAsync(Guid categoryId);
}