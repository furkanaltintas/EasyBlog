using EasyBlog.Core.Entities.Abstract;

namespace EasyBlog.Entity.Entities;

public class Image : EntityBase
{
    public Image() { }

    public Image(string fileName, string fileType)
    {
        FileName = fileName;
        FileType = fileType;
    }

    public string FileName { get; set; }
    public string FileType { get; set; }

    public virtual ICollection<AppUser> Users { get; set; } = new List<AppUser>();
    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}