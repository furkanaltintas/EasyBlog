using EasyBlog.Data.UnitOfWorks;
using EasyBlog.Entity.Entities;
using EasyBlog.Service.Services.Abstractions;

namespace EasyBlog.Service.Services.Concretes;

public class ArticleService : IArticleService
{
    private readonly IUnitOfWork _unitOfWork;

    public ArticleService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }



    public async Task<List<Article>> GetAllArticlesAsync()
    {
        return await _unitOfWork.GetRepository<Article>().GetAllAsync();
    }
}