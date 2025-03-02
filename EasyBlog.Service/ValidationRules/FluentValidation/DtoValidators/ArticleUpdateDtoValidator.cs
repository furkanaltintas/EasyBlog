using EasyBlog.Entity.DTOs.Articles;
using FluentValidation;

namespace EasyBlog.Service.ValidationRules.FluentValidation.DtoValidators;

public class ArticleUpdateDtoValidator : AbstractValidator<ArticleUpdateDto>
{
    public ArticleUpdateDtoValidator()
    {
        RuleFor(a => a.Title)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(128)
            .WithName("Başlık");

        RuleFor(a => a.Content)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(2048)
            .WithName("İçerik");
    }
}
