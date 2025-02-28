using EasyBlog.Core.Entities;

namespace EasyBlog.Entity.Entities;

public class Category : EntityBase
{
    public string Name { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}