using EasyBlog.Data.Infrastructure.Interceptors;
using EasyBlog.Entity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyBlog.Data.Context;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid, AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>
{
    private readonly AuditInterceptor _auditInterceptor;

    public AppDbContext() { }

    public AppDbContext(DbContextOptions<AppDbContext> options, AuditInterceptor auditInterceptor)
        : base(options) { _auditInterceptor = auditInterceptor; }

    #region Entities
    public DbSet<Article> Articles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Visitor> Visitors { get; set; }
    #endregion


    #region Interceptor
    // Interceptor
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.AddInterceptors(_auditInterceptor);
    #endregion


    #region Entity Konfigürasyonları
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly); // Veritabanı modelleme işlemi
    }
    #endregion
}