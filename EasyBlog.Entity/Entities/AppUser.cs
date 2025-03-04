using Microsoft.AspNetCore.Identity;

namespace EasyBlog.Entity.Entities;

public class AppUser : IdentityUser<Guid>
{
    public Guid? ImageId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public virtual Image Image { get; set; }

    public ICollection<Article> Articles { get; set; } = new List<Article>();
}