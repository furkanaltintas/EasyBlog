using EasyBlog.Core.Enums;
using EasyBlog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyBlog.Data.Mappings;

public class ImageMap : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.FileName)
            .HasMaxLength((int)MaxLength.Medium)
            .IsRequired();

        builder.Property(a => a.FileType)
            .HasMaxLength((int)MaxLength.Tiny)
            .IsRequired();



        // AppUser ve Image arasındaki ilişkiyi yapılandır
        builder.HasMany(i => i.Users)
            .WithOne(u => u.Image)
            .HasForeignKey(u => u.ImageId)
            .OnDelete(DeleteBehavior.SetNull); // Image silindiğinde ImageId NULL olur

        // Article ve Image arasındaki ilişkiyi yapılandır
        builder.HasMany(i => i.Articles)
            .WithOne(a => a.Image)
            .HasForeignKey(a => a.ImageId)
            .OnDelete(DeleteBehavior.SetNull); // Image silindiğinde ImageId NULL olur



        builder.HasData(
            new()
            {
                Id = Guid.Parse("B29B4E06-E84D-4BB2-B4D4-DC02725F8398"),
                FileName = "images/test1",
                FileType = "jpg",
                CreatedBy = "Admin User",
                CreatedDate = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.Parse("007D16D1-37D2-4400-943E-2452059151DE"),
                FileName = "images/test2",
                FileType = "jpeg",
                CreatedBy = "Admin User",
                CreatedDate = DateTime.UtcNow
            });
    }
}