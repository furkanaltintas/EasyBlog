using EasyBlog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyBlog.Data.Mappings;

public class UserRoleMap : IEntityTypeConfiguration<AppUserRole>
{
    public void Configure(EntityTypeBuilder<AppUserRole> builder)
    {
        // Primary key
        builder.HasKey(r => new { r.UserId, r.RoleId });

        // Maps to the AspNetUserRoles table
        builder.ToTable("AspNetUserRoles");



        builder.HasData(
            new AppUserRole
            {
                UserId = Guid.Parse("4FCC7985-F39B-4C50-AD1C-ADE5D0DF8279"),
                RoleId = Guid.Parse("D67D8F29-6FF0-4E7B-B582-C51B34618674")
            },
            new AppUserRole
            {
                UserId = Guid.Parse("330ADE9E-AE19-4376-9B14-FDFC3F71FB4C"),
                RoleId = Guid.Parse("40A173EA-1326-41EB-927A-3729C7277BE7")
            });
    }
}