using EasyBlog.Core.Entities.Abstract;

namespace EasyBlog.Entity.Entities;

public class Article : EntityBase
{
    public Article() { }

    public Article(string title, string content, Guid userId, Guid categoryId, Guid imageId)
    {
        Title = title;
        Content = content;
        UserId = userId;
        CategoryId = categoryId;
        ImageId = imageId;
    }

    public Guid UserId { get; set; }
    public Guid? ImageId { get; set; }
    public Guid CategoryId { get; set; }


    public string Title { get; set; }
    public string Content { get; set; }
    public int ViewCount { get; set; }


    public virtual AppUser User { get; set; }
    public virtual Image Image { get; set; }
    public virtual Category Category { get; set; }
    public ICollection<ArticleVisitor> ArticleVisitors { get; set; }
}