using EasyBlog.Core.Utilities.Results.Abstract;
using EasyBlog.Entity.Entities;

namespace EasyBlog.Service.Services.Abstractions;

public interface IRoleService
{
    Task<IDataResult<IList<AppRole>>> GetRolesAsync();
    Task<string> GetByRoleIdAsync(Guid roleId);
}