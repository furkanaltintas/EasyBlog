using EasyBlog.Core.Enums;
using EasyBlog.Entity.DTOs.Categories;
using FluentValidation;

namespace EasyBlog.Service.ValidationRules.FluentValidation.DtoValidators;

public class CategoryAddDtoValidator : AbstractValidator<CategoryAddDto>
{
    public CategoryAddDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength((int)MaxLength.Medium)
            .WithName("Kategori Adı");
    }
}
