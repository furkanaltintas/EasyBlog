using EasyBlog.Data.Infrastructure.Interceptors;
using EasyBlog.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyBlog.Data.Context;

public class AppDbContext : DbContext
{
    private readonly AuditInterceptor _auditInterceptor;

    public AppDbContext(DbContextOptions<AppDbContext> options, AuditInterceptor auditInterceptor)
        : base(options) { _auditInterceptor = auditInterceptor; }

    #region Entities
    public DbSet<Article> Articles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Image> Images { get; set; }
    #endregion


    #region Interceptor
    // Interceptor
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.AddInterceptors(_auditInterceptor);
    #endregion


    #region Entity Konfigürasyonları
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly); // Veritabanı modelleme işlemi
    #endregion
}