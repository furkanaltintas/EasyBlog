using EasyBlog.Data.Context;
using EasyBlog.Data.Infrastructure.Interceptors;
using EasyBlog.Data.Repositories.Abstractions;
using EasyBlog.Data.Repositories.Concretes;
using EasyBlog.Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyBlog.Data.Extensions;

public static class DataExtensions
{
    public static IServiceCollection LoadDataExtension(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        #region Connection
        services.AddScoped<AuditInterceptor>();

        //builder.Services.AddDbContext<AppDbContext>(options =>
        //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            options
            .UseSqlServer(configuration.GetConnectionString("DefaultConnection")) // Sql bağlantısı
            .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)); // PendingModelChangesWarning uyarısını bastırmak

            var auditInterceptor = serviceProvider.GetRequiredService<AuditInterceptor>();
            options.AddInterceptors(auditInterceptor);
        });
        #endregion

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}