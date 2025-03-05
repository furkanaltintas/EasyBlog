using EasyBlog.Service.Helpers.Images.Abstractions;
using EasyBlog.Service.Helpers.Images.Concretes;
using EasyBlog.Service.Services.Managers;
using EasyBlog.Service.ValidationRules.FluentValidation.DtoValidators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Reflection;

namespace EasyBlog.Service.Extensions;

public static class MicrosoftDependencies
{
    public static IServiceCollection LoadServiceExtension(this IServiceCollection services)
    {
        //services.AddScoped<IImageHelper, ImageHelper>();
        //services.AddScoped<IFileNameHelper, FileNameHelper>();
        //services.AddScoped<IServiceManager, ServiceManager>();

        services.AddHttpContextAccessor();

        services.AddScoped<IImageUploader, ImageUploader>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        // Tek bir yerden yönetme
        services.AddAssemblyServices(typeof(BaseService).Assembly);

        


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