using EasyBlog.Core.Enums;
using EasyBlog.Entity.Entities;
using FluentValidation;

namespace EasyBlog.Service.ValidationRules.FluentValidation;

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength((int)MaxLength.Medium)
            .WithName("Kategori Adı");
    }
}