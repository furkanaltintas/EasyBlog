using EasyBlog.Core.Entities.Abstract;
using EasyBlog.Entity.DTOs.Categories;

namespace EasyBlog.Entity.DTOs.Articles
{
    public class ArticleUpdateDto : IDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid CategoryId { get; set; }
        public IList<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    }
}