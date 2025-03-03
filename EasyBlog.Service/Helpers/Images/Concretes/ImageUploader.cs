using EasyBlog.Service.Helpers.Images.Abstractions;
using EasyBlog.Service.Helpers.Images.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace EasyBlog.Service.Helpers.Images.Concretes;

public class ImageUploader : IImageUploader
{
    private readonly IWebHostEnvironment _env;

    public ImageUploader(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> UploadImage(IFormFile imageFile, string folderName, string newFileName)
    {
        string path = Path.Combine(_env.WebRootPath, ImageConstants.ImageFolder, folderName);

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        string fullPath = Path.Combine(path, newFileName);

        await using var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, false);
        await imageFile.CopyToAsync(stream);
        await stream.FlushAsync();

        return $"{folderName}/{newFileName}";
    }

    public void DeleteImage(string imageName)
    {
        string fileToDelete = Path.Combine(_env.WebRootPath, ImageConstants.ImageFolder, imageName);

        if (File.Exists(fileToDelete))
            File.Delete(fileToDelete);
    }
}
