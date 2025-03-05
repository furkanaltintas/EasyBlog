using EasyBlog.Core.Entities.Abstract;

namespace EasyBlog.Entity.Entities;

public class ArticleVisitor : IEntityBase
{
    public ArticleVisitor()
    {
        
    }

    public ArticleVisitor(Guid articleId, Guid visitorId)
    {
        ArticleId = articleId;
        VisitorId = visitorId;
    }

    public Guid ArticleId { get; set; }
    public Article Article { get; set; }

    public Guid VisitorId { get; set; }
    public Visitor Visitor { get; set; }
}
