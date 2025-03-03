using Castle.DynamicProxy;
using FluentValidation;

namespace EasyBlog.Service.Aspects;

public class ValidationAspect : IInterceptor
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationAspect(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Intercept(IInvocation invocation)
    {
        foreach (var argument in invocation.Arguments)
        {
            var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
            var validator = _serviceProvider.GetService(validatorType) as IValidator;
            if (validator != null)
            {
                var validationResult = validator.Validate(new ValidationContext<object>(argument));
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage)));
                }
            }
        }

        invocation.Proceed(); // Eğer doğrulama başarılıysa metodu çalıştır
    }
}