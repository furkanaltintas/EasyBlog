using Microsoft.AspNetCore.Http;

namespace EasyBlog.Service.Helpers.Images.Abstractions;

public interface IImageUploader
{
    Task<string> UploadImage(IFormFile imageFile, string folderName, string newFileName);
    void DeleteImage(string imageName);
}