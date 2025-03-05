using EasyBlog.Core.Enums;
using EasyBlog.Entity.DTOs.Articles;
using FluentValidation;

namespace EasyBlog.Service.ValidationRules.FluentValidation.DtoValidators;

public class ArticleAddDtoValidator : AbstractValidator<ArticleAddDto>
{
    public ArticleAddDtoValidator()
    {
        RuleFor(a => a.Title)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength((int)MaxLength.Medium)
            .WithName("Başlık");

        RuleFor(a => a.Content)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength((int)MaxLength.MaximumLong)
            .WithName("İçerik");
    }
}