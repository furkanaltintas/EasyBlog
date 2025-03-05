using EasyBlog.Service.Services.Managers;
using Microsoft.AspNetCore.Mvc;

namespace EasyBlog.Web.ViewComponents;

public class WelcomeManagementViewComponent : ViewComponent
{
    private readonly IBaseService _service;

    public WelcomeManagementViewComponent(IBaseService service)
    {
        _service = service;
    }


    public async Task<IViewComponentResult> InvokeAsync()
    {
        var result = await _service.UserService.GetUserAsync();

        return View(result.Data);
    }
}
