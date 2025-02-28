using EasyBlog.Core.Enums;
using EasyBlog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyBlog.Data.Mappings;

public class CategoryMap : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .HasMaxLength((int)MaxLength.Medium)
            .IsRequired();

        builder.HasData(new Category
        {
            Id = Guid.Parse("58456306-9248-4CD3-B0AA-F7C9C53C5D5E"),
            Name = "Lorem Ipsum Category 1",
            CreatedBy = "Admin Test",
            CreatedDate = DateTime.UtcNow
        },
        new Category
        {
            Id = Guid.Parse("D0A592EE-589E-4D43-A83D-0E60DC239368"),
            Name = "Lorem Ipsum Category 2",
            CreatedBy = "Admin Test",
            CreatedDate = DateTime.UtcNow
        });
    }
}