using EasyBlog.Core.Utilities.Results.Abstract;
using EasyBlog.Core.Utilities.Results.ComplexTypes;
using EasyBlog.Core.Utilities.Results.Concrete;
using EasyBlog.Entity.Entities;
using EasyBlog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EasyBlog.Service.Services.Concretes
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IDataResult<IList<AppRole>>> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return new DataResult<IList<AppRole>>(ResultStatus.Success, roles);
        }


        public async Task<string> GetByRoleIdAsync(Guid roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            return role.ToString();
        }
    }
}