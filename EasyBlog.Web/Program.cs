using EasyBlog.Data.Extensions;
using EasyBlog.Service.Extensions;
using EasyBlog.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllersWithViews()
    .AddRazorRuntimeCompilation();


builder.Services.AddHttpContextAccessor(); // IHttpContextAccessor servisini ekliyoruz

#region Extensions
builder.Services.LoadDataExtension(builder.Configuration);
builder.Services.LoadServiceExtension();
#endregion

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true; // Küçük harf zorunluluðu
    options.AppendTrailingSlash = false; // URL sonunda '/' ifadesi olmasýn
});

var app = builder.Build();

app.UseMiddleware<LowercaseUrlMiddleware>();


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