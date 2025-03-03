using EasyBlog.Data.Context;
using EasyBlog.Data.Extensions;
using EasyBlog.Entity.Entities;
using EasyBlog.Service.Extensions;
using EasyBlog.Web.Middleware;
using Microsoft.AspNetCore.Identity;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSession();

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

#region Extensions
builder.Services.LoadDataExtension(builder.Configuration);
builder.Services.LoadServiceExtension();
#endregion


#region Identity & Cookie
builder.Services.AddIdentity<AppUser, AppRole>(action =>
{
    action.Password.RequireNonAlphanumeric = false;
    action.Password.RequireLowercase = false;
    action.Password.RequireUppercase = false;
})
    .AddRoleManager<RoleManager<AppRole>>()
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


var app = builder.Build();

app.UseMiddleware<LowercaseUrlMiddleware>(); // Küçük harf zorunluluðu


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseNToastNotify();


app.UseHttpsRedirection();
app.UseSession();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication(); // Sisteme login oldu mu ?
app.UseAuthorization(); // Yetkisi var mý ?

app.MapStaticAssets();

#region Endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "Management",
        areaName: "Management",
        pattern: "management/{controller=home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=home}/{action=Index}/{id?}");
});
#endregion


app.Run();