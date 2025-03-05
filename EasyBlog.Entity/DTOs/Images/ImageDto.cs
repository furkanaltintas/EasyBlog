using EasyBlog.Core.Entities.Abstract;

namespace EasyBlog.Entity.DTOs.Images;

public class ImageDto : IDto
{
    public string FileName { get; set; }
}