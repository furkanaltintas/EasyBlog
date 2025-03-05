using Microsoft.AspNetCore.Mvc;

namespace EasyBlog.Web.Areas.Management.ViewComponents
{
    public class DashboardTitleViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
