using AutoMapper;
using EasyBlog.Core.Enums;
using EasyBlog.Data.UnitOfWorks;
using EasyBlog.Entity.Entities;
using EasyBlog.Service.Helpers.Images.Abstractions;
using EasyBlog.Service.Services.Abstractions;
using EasyBlog.Service.Services.Managers;
using Microsoft.AspNetCore.Http;

namespace EasyBlog.Service.Services.Concretes;

public class ImageService : RepositoryService, IImageService
{
    private readonly IImageHelper _imageHelper;

    public ImageService(IMapper mapper, IUnitOfWork unitOfWork, IImageHelper imageHelper) : base(mapper, unitOfWork)
    {
        _imageHelper = imageHelper;
    }

    public void Delete(string imageName)
    {
        _imageHelper.Delete(imageName);
    }

    public async Task<Guid> PhotoUploadAsync(string title, IFormFile photo, ImageType imageType)
    {
        var imageUpload = await _imageHelper.Upload(title, photo, imageType);
        Image image = new(imageUpload.FullName, photo.ContentType);
        await _unitOfWork.GetRepository<Image>().AddAsync(image);

        return image.Id;
    }
}