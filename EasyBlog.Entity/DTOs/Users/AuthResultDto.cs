using EasyBlog.Core.Entities.Abstract;

namespace EasyBlog.Entity.DTOs.Users;

public class AuthResultDto : IDto
{
    public string Message { get; set; }
    public bool Success { get; set; }
}