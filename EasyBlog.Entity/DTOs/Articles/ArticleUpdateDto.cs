using EasyBlog.Core.Entities.Abstract;
using EasyBlog.Entity.DTOs.Categories;
using EasyBlog.Entity.Entities;
using Microsoft.AspNetCore.Http;

namespace EasyBlog.Entity.DTOs.Articles
{
    public class ArticleUpdateDto : IDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid CategoryId { get; set; }

        public Image Image { get; set; } = new();
        public IFormFile? Photo { get; set; }

        public virtual IList<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    }
}