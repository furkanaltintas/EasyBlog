using EasyBlog.Service.Services.Managers;
using Microsoft.AspNetCore.Mvc;

namespace EasyBlog.Web.ViewComponents
{
    public class HomeCategoriesViewComponent : ViewComponent
    {
        private readonly IBaseService _service;

        public HomeCategoriesViewComponent(IBaseService service)
        {
            _service = service;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _service.CategoryService.GetAllCategoriesNonDeletedAsync();

            return View(result.Data);
        }
    }
}
