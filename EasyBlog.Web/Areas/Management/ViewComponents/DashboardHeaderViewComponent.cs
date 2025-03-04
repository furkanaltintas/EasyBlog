using AutoMapper;
using EasyBlog.Entity.DTOs.Users;
using EasyBlog.Entity.Entities;
using EasyBlog.Service.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyBlog.Web.Areas.Management.ViewComponents
{
    public class DashboardHeaderViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public DashboardHeaderViewComponent(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loggedInUser = await _userManager.Users.Include(u => u.Image).FirstOrDefaultAsync(u => u.Id == LoggedInUserExtensions.GetLoggedInUserId(HttpContext.User));
            var roles = string.Join(", ", await _userManager.GetRolesAsync(loggedInUser));

            var userDto = _mapper.Map<UserDto>(loggedInUser);
            userDto.Role = roles;
            return View(userDto);
        }
    }
}
