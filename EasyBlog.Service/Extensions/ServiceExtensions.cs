using EasyBlog.Service.Services.Abstractions;
using EasyBlog.Service.Services.Concretes;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyBlog.Service.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection LoadServiceExtension(this IServiceCollection services)
    {
        services.AddScoped<IArticleService, ArticleService>();

        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(assembly);
        return services;
    }
}