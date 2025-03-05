using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using EasyBlog.Data.Context;
using EasyBlog.Data.Extensions;
using EasyBlog.Entity.Entities;
using EasyBlog.Service.Aspects;
using EasyBlog.Service.Describers;
using EasyBlog.Service.Extensions;
using EasyBlog.Service.Services.Concretes;
using EasyBlog.Service.ValidationRules.FluentValidation;
using EasyBlog.Web.Middleware;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);

#region Autofac
// Autofac Kullanýmý
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(contaier =>
{
    contaier.RegisterType<ValidationAspect>(); // Aspect'i kaydet
    contaier.RegisterType<CacheAspect>();

    // Tüm servisleri dinamik proxy olarak kaydet (Aspect'leri çalýþtýrabilmek için)
    contaier.RegisterAssemblyTypes(typeof(CategoryService).Assembly)
    .AsImplementedInterfaces()
    .EnableInterfaceInterceptors()
    .InterceptedBy(typeof(ValidationAspect));

    // FluentValidation Validator'larý otomatik ekle
    contaier.RegisterAssemblyTypes(typeof(CategoryValidator).Assembly)
    .Where(p => p.IsAssignableTo(typeof(IValidator)))
    .AsImplementedInterfaces();
});
#endregion

#region Extensions
builder.Services.LoadDataExtension(builder.Configuration);
builder.Services.LoadServiceExtension();
#endregion

#region Identity & Cookie
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
    .AddRoleManager<RoleManager<AppRole>>()
    .AddErrorDescriber<CustomIdentityErrorDescriber>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


// Kimlik Doðrulama çerezlerini özelleþtirme iþlemi
builder.Services.ConfigureApplicationCookie(configure =>
{
    configure.LoginPath = new PathString("/Management/Auth/Login"); // Kullanýcýnýn kimliði doðrulanmamýþsa, login sayfasýna yönlendir.
    configure.LogoutPath = new PathString("/Management/Auth/Logout"); // Kullanýcý sistemden çýkýþ yaptýðýnda, çýkýþ iþlemi bu url üzerinden yapýlýr
    configure.Cookie = new CookieBuilder
    {
        Name = "EasyBlogCookie", // Çerezin adý tarayýcýda bu isimle saklanacak
        HttpOnly = true, // Çerez yalnýzca sunucu tarafýndan okunabilir, JavaScript eriþemez ve XSS saldýrýlarýna karþý ek güvenlik saðlar.
        SameSite = SameSiteMode.Strict, // Çerez, sadece ayný site içindeki isteklerle gönderilir. Üçüncü taraf sitelerden eriþim engellenir (CSRF saldýrýlarýna karþý güvenlik saðlar).
        SecurePolicy = CookieSecurePolicy.SameAsRequest // Çerez, HTTPS isteklerinde güvenli olarak gönderilir, HTTP içinse duruma baðlýdýr. Eðer HTTPS kullanýlýyorsa çerez güvenli olur.
    };
    configure.SlidingExpiration = true; // Kullanýcý aktif oldukça çerez süresi uzar.
    configure.ExpireTimeSpan = TimeSpan.FromDays(7); // Çerezin maksimum ömrünü belirler.
    configure.AccessDeniedPath = new PathString("/Management/Auth/AccessDenied"); // Yetkisiz bir sayfaya eriþmeye çalýþan kullanýcý, bu sayfaya yönlendirilir.
});
#endregion

#region ToastNotification
builder.Services
    .AddControllersWithViews()
    .AddNToastNotifyToastr(new()
    {
        PositionClass = ToastPositions.TopRight,
        Title = "Baþarýlý Ýþlem!",
        TimeOut = 5000,
        ProgressBar = true,
        CloseButton = true
    })
    .AddRazorRuntimeCompilation();
#endregion

builder.Services.AddSession(); // Session ekleme


var app = builder.Build();
app.UseMiddleware<LowercaseUrlMiddleware>(); // Küçük harf zorunluluðu

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseNToastNotify();


app.UseHttpsRedirection();
app.UseSession();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication(); // Sisteme login oldu mu ?
app.UseAuthorization(); // Yetkisi var mý ?




// Yönlendirme enpoint'leri
app.MapStaticAssets();
#region Endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "Management",
        areaName: "Management",
        pattern: "management/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
#endregion

app.Run();