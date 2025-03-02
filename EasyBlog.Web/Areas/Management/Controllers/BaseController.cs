using EasyBlog.Service.Services.Managers;
using Microsoft.AspNetCore.Mvc;

namespace EasyBlog.Web.Areas.Management.Controllers;

[Area("Management")]
public class BaseController : Controller
{
    protected readonly IServiceManager _serviceManager;

    public BaseController(IServiceManager serviceManager) { _serviceManager = serviceManager; }
}