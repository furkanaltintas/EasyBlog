using EasyBlog.Entity.Entities;

namespace EasyBlog.Entity.DTOs.Users;

public class UserAddDto
{
    public UserAddDto()
    {
        
    }

    public UserAddDto(IList<AppRole> roles)
    {
        Roles = roles;
    }

    public Guid RoleId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }

    public IList<AppRole> Roles { get; set; } = new List<AppRole>();

}