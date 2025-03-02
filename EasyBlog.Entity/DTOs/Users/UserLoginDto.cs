using EasyBlog.Core.Entities;

namespace EasyBlog.Entity.DTOs.Users;

public class UserLoginDto : IDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}