using EasyBlog.Data.Context;
using EasyBlog.Data.Infrastructure.Interceptors;
using EasyBlog.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Connection
builder.Services.AddHttpContextAccessor(); // IHttpContextAccessor servisini ekliyoruz
builder.Services.AddScoped<AuditInterceptor>();

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    options
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")) // Sql bağlantısı
    .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)); // PendingModelChangesWarning uyarısını bastırmak

    var auditInterceptor = serviceProvider.GetRequiredService<AuditInterceptor>();
    options.AddInterceptors(auditInterceptor);
});
#endregion

builder.Services.LoadDataExtensions(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();