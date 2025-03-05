using EasyBlog.Core.Enums;
using EasyBlog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyBlog.Data.Mappings;

public class ArticleMap : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(a => a.Id);

        builder
            .Property(a => a.Title)
            .HasMaxLength((int)MaxLength.Medium)
            .IsRequired();

        builder
            .Property(a => a.Content)
            .HasMaxLength((int)MaxLength.MaximumLong)
            .IsRequired();

        builder
            .HasOne(a => a.Image)
            .WithMany(i => i.Articles)
            .HasForeignKey(a => a.ImageId)
            .OnDelete(DeleteBehavior.SetNull);

        // Örnek Data
        builder.HasData(new Article
        {
            Id = Guid.NewGuid(),
            CategoryId = Guid.Parse("58456306-9248-4CD3-B0AA-F7C9C53C5D5E"),
            ImageId = Guid.Parse("10ad367a-71b2-4143-af5f-2400ad04ac50"),
            UserId = Guid.Parse("4FCC7985-F39B-4C50-AD1C-ADE5D0DF8279"),
            Title = "Lorem Ipsum 1",
            Content = "Lorem Ipsum 1 is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
            ViewCount = 15,
            CreatedBy = "Admin User",
            CreatedDate = DateTime.UtcNow
        },
        new Article
        {
            Id = Guid.NewGuid(),
            CategoryId = Guid.Parse("D0A592EE-589E-4D43-A83D-0E60DC239368"),
            ImageId = Guid.Parse("007d16d1-37d2-4400-943e-2452059151de"),
            UserId = Guid.Parse("330ADE9E-AE19-4376-9B14-FDFC3F71FB4C"),
            Title = "Lorem Ipsum 2",
            Content = "Lorem Ipsum 2 is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
            ViewCount = 15,
            CreatedBy = "Admin User",
            CreatedDate = DateTime.UtcNow
        });
    }
}