using EasyBlog.Entity.Entities;

namespace EasyBlog.Entity.DTOs.Users;

public class UserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }

    public Image Image { get; set; }
}