using EasyBlog.Data.UnitOfWorks;
using EasyBlog.Entity.Entities;
using EasyBlog.Service.Services.Abstractions;

namespace EasyBlog.Service.Services.Concretes;

public class ArticleVisitorService : IArticleVisitorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;

    public ArticleVisitorService(IUnitOfWork unitOfWork, Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task CreateArticleVisitorAsync(Guid articleId)
    {
        var ipAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.MapToIPv4().ToString();
        var articleVisitors = await _unitOfWork.GetRepository<ArticleVisitor>().GetAllAsync(null, a => a.Visitor, a => a.Article);
        var article = await _unitOfWork.GetRepository<Article>().GetAsync(a => a.Id == articleId);
        var visitor = await _unitOfWork.GetRepository<Visitor>().GetAsync(v => v.IpAddress == ipAddress);
        var addArticleVisitors = new ArticleVisitor(article.Id, visitor.Id);

        if (articleVisitors.Any(a => a.VisitorId == addArticleVisitors.VisitorId && a.ArticleId == addArticleVisitors.ArticleId))
            return;

        await _unitOfWork.GetRepository<ArticleVisitor>().AddAsync(addArticleVisitors);
        article.ViewCount += 1;
        await _unitOfWork.GetRepository<Article>().UpdateAsync(article);
        await _unitOfWork.SaveAsync();
        return;
    }
}