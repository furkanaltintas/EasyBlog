using EasyBlog.Core.Enums;
using EasyBlog.Entity.DTOs.Images;
using Microsoft.AspNetCore.Http;

namespace EasyBlog.Service.Helpers.Images.Abstractions;

public interface IImageHelper
{
    Task<ImageUploadedDto> Upload(string name, IFormFile imageFile, ImageType imageType, string? folderName = null);
    void Delete(string imageName);
}