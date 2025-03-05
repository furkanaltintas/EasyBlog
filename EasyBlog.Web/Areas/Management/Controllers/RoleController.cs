using EasyBlog.Service.Services.Managers;
using EasyBlog.Web.Areas.Management.Controllers.Base;
using EasyBlog.Web.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyBlog.Web.Areas.Management.Controllers;

[Authorize(Roles = $"{RoleConsts.SuperAdmin}")]
[Route(RouteConstants.Role)]
public class RoleController : BaseController
{
    public RoleController(IBaseService serviceManager) : base(serviceManager)
    {
    }

    public async Task<IActionResult> Index()
    {
        var result = await _serviceManager.RoleService.GetRolesAsync();
        return View(result.Data);
    }
}