using EasyBlog.Core.Entities.Abstract;

namespace EasyBlog.Entity.DTOs.Images
{
    public class ImageUploadedDto : IDto
    {
        public ImageUploadedDto() { }

        public ImageUploadedDto(string fullName) { FullName = fullName; }

        public string FullName { get; set; } = string.Empty;
    }
}