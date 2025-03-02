using EasyBlog.Core.Entities;

namespace EasyBlog.Entity.Entities;

public class Category : EntityBase
{
    public Category() { }

    public Category(string name)
    {
        Name = name;
    }

    public string Name { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}