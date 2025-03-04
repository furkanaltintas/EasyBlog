using EasyBlog.Entity.DTOs.Users;
using FluentValidation;

namespace EasyBlog.Service.ValidationRules.FluentValidation;

public class UserValidator : AbstractValidator<UserAddDto>
{
    public UserValidator()
    {
        RuleFor(u => u.FirstName)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(50)
            .WithName("Ad");

        RuleFor(u => u.LastName)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(50)
            .WithName("Soyad");

        RuleFor(u => u.PhoneNumber)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(11)
            .WithName("Telefon Numarası");
    }
}