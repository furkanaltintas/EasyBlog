﻿using EasyBlog.Core.Enums;
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
}