using EasyBlog.Core.Entities;

namespace EasyBlog.Entity.DTOs.Users;

public class AuthResultDto : IDto
{
    public string Message { get; set; }
    public bool Success { get; set; }
}