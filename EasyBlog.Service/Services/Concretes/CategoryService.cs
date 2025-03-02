using AutoMapper;
using EasyBlog.Data.UnitOfWorks;
using EasyBlog.Entity.DTOs.Categories;
using EasyBlog.Entity.Entities;
using EasyBlog.Service.Services.Abstractions;

namespace EasyBlog.Service.Services.Concretes;

public class CategoryService : RepositoryService, ICategoryService
{
    public CategoryService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork) { }

    public async Task<IList<CategoryDto>> GetAllCategoriesNonDeletedAsync()
    {
        var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync(c => !c.IsDeleted);
        return _mapper.Map<List<CategoryDto>>(categories);
    }
}