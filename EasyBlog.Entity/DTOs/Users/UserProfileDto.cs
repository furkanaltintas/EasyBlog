using EasyBlog.Entity.Entities;
using Microsoft.AspNetCore.Http;

namespace EasyBlog.Entity.DTOs.Users;

public class UserProfileDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string CurrentPassword { get; set; }
    public string? NewPassword { get; set; }


    public Image Image { get; set; }
    public IFormFile? Photo { get; set; }
}