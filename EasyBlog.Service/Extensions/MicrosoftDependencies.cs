using EasyBlog.Service.Services.Abstractions;
using EasyBlog.Service.Services.Concretes;
using EasyBlog.Service.Services.Managers;
using EasyBlog.Service.ValidationRules.FluentValidation.DtoValidators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Reflection;

namespace EasyBlog.Service.Extensions;

public static class MicrosoftDependencies
{
    public static IServiceCollection LoadServiceExtension(this IServiceCollection services)
    {
        // Tek bir yerden yönetme
        services.AddAssemblyServices(typeof(BaseService).Assembly);
        //services.AddScoped<IServiceManager, ServiceManager>();


        #region AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        #endregion


        #region FluentValidation
        services
            .AddControllersWithViews().AddFluentValidation(configure =>
            {
                configure.RegisterValidatorsFromAssemblyContaining<ArticleAddDtoValidator>();
                configure.DisableDataAnnotationsValidation = false;
                configure.ValidatorOptions.LanguageManager.Culture = new CultureInfo("tr");
            });
        #endregion

        #region Route
        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true; // Küçük harf zorunluluğu
            options.AppendTrailingSlash = false; // URL sonunda '/' ifadesi olmasın
        });
        #endregion

        return services;
    }
}