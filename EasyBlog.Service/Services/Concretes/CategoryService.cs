using Autofac.Extras.DynamicProxy;
using AutoMapper;
using EasyBlog.Core.Utilities.Results.Abstract;
using EasyBlog.Core.Utilities.Results.ComplexTypes;
using EasyBlog.Core.Utilities.Results.Concrete;
using EasyBlog.Data.UnitOfWorks;
using EasyBlog.Entity.DTOs.Categories;
using EasyBlog.Entity.Entities;
using EasyBlog.Service.Aspects;
using EasyBlog.Service.Services.Abstractions;
using EasyBlog.Service.Services.Managers;
using EasyBlog.Service.Utilities;

namespace EasyBlog.Service.Services.Concretes;

[Intercept(typeof(ValidationAspect))]
[Intercept(typeof(CacheAspect))]
public class CategoryService : RepositoryService, ICategoryService
{
    public CategoryService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork) { }

    public async Task<IResult> CreateCategoryAsync(CategoryAddDto categoryAddDto)
    {
        var category = _mapper.Map<Category>(categoryAddDto);

        await _unitOfWork.GetRepository<Category>().AddAsync(category);
        await _unitOfWork.SaveAsync();

        return new Result(ResultStatus.Success, Messages.Category.Add(categoryAddDto.Name));
    }

    public async Task<IDataResult<IList<CategoryListDto>>> GetAllCategoriesDeletedAsync()
    {
        var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync(c => c.IsDeleted);
        var categoryListDtos = _mapper.Map<List<CategoryListDto>>(categories);

        return new DataResult<List<CategoryListDto>>(ResultStatus.Success, categoryListDtos);
    }

    public async Task<IDataResult<IList<CategoryListDto>>> GetAllCategoriesNonDeletedAsync()
    {
        var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync(c => !c.IsDeleted);
        var categoryListDtos = _mapper.Map<List<CategoryListDto>>(categories);

        return new DataResult<List<CategoryListDto>>(ResultStatus.Success, categoryListDtos);
    }

    public async Task<IDataResult<CategoryDto>> GetCategoryByGuid(Guid categoryId)
    {
        var category = await _unitOfWork.GetRepository<Category>().GetAsync(c => c.Id == categoryId && !c.IsDeleted);

        if (category == null)
            return new DataResult<CategoryDto>(ResultStatus.Error, Messages.Category.NotFoundById(categoryId), null);

        var categoryDto = _mapper.Map<CategoryDto>(category);
        return new DataResult<CategoryDto>(ResultStatus.Success, categoryDto);
    }

    public async Task<IDataResult<CategoryUpdateDto>> GetCategoryByUpdateGuidAsync(Guid categoryId)
    {
        var category = await _unitOfWork.GetRepository<Category>().GetAsync(c => c.Id == categoryId && !c.IsDeleted);

        if (category == null)
            return new DataResult<CategoryUpdateDto>(ResultStatus.Error, Messages.Category.NotFoundById(categoryId), null);

        var categoryUpdateDto = _mapper.Map<CategoryUpdateDto>(category);
        return new DataResult<CategoryUpdateDto>(ResultStatus.Success, categoryUpdateDto);
    }

    public async Task<IResult> SafeDeleteCategoryAsync(Guid categoryId)
    {
        var category = await _unitOfWork.GetRepository<Category>().GetAsync(c => c.Id == categoryId);

        if (category == null)
            return new Result(ResultStatus.Error, Messages.Category.NotFoundById(categoryId));


        await _unitOfWork.GetRepository<Category>().DeleteAsync(category);
        await _unitOfWork.SaveAsync();
        return new Result(ResultStatus.Success, Messages.Category.Delete(category.Name));
    }

    public async Task<IResult> UndoDeleteCategoryAsync(Guid categoryId)
    {
        var category = await _unitOfWork.GetRepository<Category>().GetAsync(c => c.Id == categoryId);

        if (category == null)
            return new Result(ResultStatus.Error, Messages.Category.NotFoundById(categoryId));

        category.IsDeleted = false;
        category.DeletedDate = null;
        category.DeletedBy = null;

        await _unitOfWork.GetRepository<Category>().UpdateAsync(category);
        await _unitOfWork.SaveAsync();
        return new Result(ResultStatus.Success, Messages.Category.UndoDelete(category.Name));
    }

    public async Task<IResult> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto, Guid categoryId)
    {
        var category = await _unitOfWork.GetRepository<Category>().GetByGuidAsync(categoryId);

        if (category == null)
            return new Result(ResultStatus.Error, Messages.Category.NotFound(false));

        categoryUpdateDto.Id = categoryId;
        _mapper.Map(categoryUpdateDto, category);

        await _unitOfWork.GetRepository<Category>().UpdateAsync(category);
        await _unitOfWork.SaveAsync();

        return new Result(ResultStatus.Success, Messages.Category.Update(categoryUpdateDto.Name));
    }
}