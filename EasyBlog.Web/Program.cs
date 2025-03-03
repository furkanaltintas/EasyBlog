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
        Title = "Ba�ar�l� ��lem!",
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


// Kimlik Do�rulama �erezlerini �zelle�tirme i�lemi
builder.Services.ConfigureApplicationCookie(configure =>
{
    configure.LoginPath = new PathString("/Management/Auth/Login"); // Kullan�c�n�n kimli�i do�rulanmam��sa, login sayfas�na y�nlendir.
    configure.LogoutPath = new PathString("/Management/Auth/Logout"); // Kullan�c� sistemden ��k�� yapt���nda, ��k�� i�lemi bu url �zerinden yap�l�r
    configure.Cookie = new CookieBuilder
    {
        Name = "EasyBlogCookie", // �erezin ad� taray�c�da bu isimle saklanacak
        HttpOnly = true, // �erez yaln�zca sunucu taraf�ndan okunabilir, JavaScript eri�emez ve XSS sald�r�lar�na kar�� ek g�venlik sa�lar.
        SameSite = SameSiteMode.Strict, // �erez, sadece ayn� site i�indeki isteklerle g�nderilir. ���nc� taraf sitelerden eri�im engellenir (CSRF sald�r�lar�na kar�� g�venlik sa�lar).
        SecurePolicy = CookieSecurePolicy.SameAsRequest // �erez, HTTPS isteklerinde g�venli olarak g�nderilir, HTTP i�inse duruma ba�l�d�r. E�er HTTPS kullan�l�yorsa �erez g�venli olur.
    };
    configure.SlidingExpiration = true; // Kullan�c� aktif olduk�a �erez s�resi uzar.
    configure.ExpireTimeSpan = TimeSpan.FromDays(7); // �erezin maksimum �mr�n� belirler.
    configure.AccessDeniedPath = new PathString("/Management/Auth/AccessDenied"); // Yetkisiz bir sayfaya eri�meye �al��an kullan�c�, bu sayfaya y�nlendirilir.
});
#endregion


var app = builder.Build();

app.UseMiddleware<LowercaseUrlMiddleware>(); // K���k harf zorunlulu�u


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
app.UseAuthorization(); // Yetkisi var m� ?

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