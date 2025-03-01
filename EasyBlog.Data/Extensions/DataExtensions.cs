using EasyBlog.Data.Repositories.Abstractions;
using EasyBlog.Data.Repositories.Concretes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyBlog.Data.Extensions;

public static class DataExtensions
{
    public static IServiceCollection LoadDataExtensions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }
}