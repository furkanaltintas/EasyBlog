using EasyBlog.Core.Enums;
using EasyBlog.Entity.DTOs.Images;
using EasyBlog.Service.Helpers.Images.Abstractions;
using EasyBlog.Service.Helpers.Images.Constants;
using Microsoft.AspNetCore.Http;

namespace EasyBlog.Service.Helpers.Images.Concretes;

public class ImageHelper : IImageHelper
{
    private readonly IFileNameHelper _fileNameHelper;
    private readonly IImageUploader _imageUploader;

    public ImageHelper(IFileNameHelper fileNameHelper, IImageUploader imageUploader)
    {
        _fileNameHelper = fileNameHelper;
        _imageUploader = imageUploader;
    }

    public void Delete(string imageName)
    {
        _imageUploader.DeleteImage(imageName);
    }

    public async Task<ImageUploadedDto> Upload(string name, IFormFile imageFile, ImageType imageType, string? folderName = null)
    {
        folderName ??= imageType == ImageType.User ? ImageConstants.UserImagesFolder : ImageConstants.ArticleImagesFolder;
        string newFileName = _fileNameHelper.GenerateFileName(name, imageFile.FileName);

        string uploadedPath = await _imageUploader.UploadImage(imageFile, folderName, newFileName);

        return new ImageUploadedDto(uploadedPath);
    }



    private string ReplaceInvalidChars(string fileName)
    {
        return fileName.Replace("İ", "I")
                 .Replace("ı", "i")
                 .Replace("Ğ", "G")
                 .Replace("ğ", "g")
                 .Replace("Ü", "U")
                 .Replace("ü", "u")
                 .Replace("ş", "s")
                 .Replace("Ş", "S")
                 .Replace("Ö", "O")
                 .Replace("ö", "o")
                 .Replace("Ç", "C")
                 .Replace("ç", "c")
                 .Replace("é", "")
                 .Replace("!", "")
                 .Replace("'", "")
                 .Replace("^", "")
                 .Replace("+", "")
                 .Replace("%", "")
                 .Replace("/", "")
                 .Replace("(", "")
                 .Replace(")", "")
                 .Replace("=", "")
                 .Replace("?", "")
                 .Replace("_", "")
                 .Replace("*", "")
                 .Replace("æ", "")
                 .Replace("ß", "")
                 .Replace("@", "")
                 .Replace("€", "")
                 .Replace("<", "")
                 .Replace(">", "")
                 .Replace("#", "")
                 .Replace("$", "")
                 .Replace("½", "")
                 .Replace("{", "")
                 .Replace("[", "")
                 .Replace("]", "")
                 .Replace("}", "")
                 .Replace(@"\", "")
                 .Replace("|", "")
                 .Replace("~", "")
                 .Replace("¨", "")
                 .Replace(",", "")
                 .Replace(";", "")
                 .Replace("`", "")
                 .Replace(".", "")
                 .Replace(":", "")
                 .Replace(" ", "");
    }
}