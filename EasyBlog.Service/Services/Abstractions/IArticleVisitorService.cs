namespace EasyBlog.Service.Services.Abstractions;

public interface IArticleVisitorService
{
    Task CreateArticleVisitorAsync(Guid articleId);
}