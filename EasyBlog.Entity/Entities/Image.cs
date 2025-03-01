using EasyBlog.Core.Entities;

namespace EasyBlog.Entity.Entities;

public class Image : EntityBase
{
    public string FileName { get; set; }
    public string FileType { get; set; }


    public virtual ICollection<AppUser> Users { get; set; } = new List<AppUser>();
    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}