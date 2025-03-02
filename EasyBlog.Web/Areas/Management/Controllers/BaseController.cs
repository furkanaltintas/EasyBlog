using EasyBlog.Service.Services.Managers;
using Microsoft.AspNetCore.Mvc;

namespace EasyBlog.Web.Areas.Management.Controllers;

[Area("Management")]
public class BaseController : Controller
{
    protected readonly IBaseService _serviceManager;

    public BaseController(IBaseService serviceManager) { _serviceManager = serviceManager; }
}