using EasyBlog.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace EasyBlog.Service.Services.Abstractions
{
    public interface IImageService
    {
        Task<Guid> PhotoUploadAsync(string title, IFormFile photo, ImageType imageType);
        void Delete(string imageName);
    }
}