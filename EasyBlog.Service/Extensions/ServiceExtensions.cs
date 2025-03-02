using EasyBlog.Service.Services.Abstractions;
using EasyBlog.Service.Services.Concretes;
using EasyBlog.Service.Services.Managers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyBlog.Service.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection LoadServiceExtension(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();


        services.AddScoped<IServiceManager, ServiceManager>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}