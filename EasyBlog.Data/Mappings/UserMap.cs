using EasyBlog.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyBlog.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        // Primary key
        builder.HasKey(u => u.Id);

        // Indexes for "normalized" username and email, to allow efficient lookups
        builder.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
        builder.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

        // Maps to the AspNetUsers table
        builder.ToTable("AspNetUsers");

        // A concurrency token for use with the optimistic concurrency checking
        builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

        // Limit the size of columns to use efficient database types
        builder.Property(u => u.UserName).HasMaxLength(256);
        builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
        builder.Property(u => u.Email).HasMaxLength(256);
        builder.Property(u => u.NormalizedEmail).HasMaxLength(256);

        // The relationships between User and other entity types
        // Note that these relationships are configured with no navigation properties

        // Each User can have many UserClaims
        builder.HasMany<AppUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

        // Each User can have many UserLogins
        builder.HasMany<AppUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

        // Each User can have many UserTokens
        builder.HasMany<AppUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

        // Each User can have many entries in the UserRole join table
        builder.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();


        builder
            .HasOne(u => u.Image)
            .WithMany(i => i.Users)
            .HasForeignKey(a => a.ImageId)
            .OnDelete(DeleteBehavior.SetNull);

        #region Data
        // SuperAdmin
        var superAdmin = new AppUser
        {
            Id = Guid.Parse("4FCC7985-F39B-4C50-AD1C-ADE5D0DF8279"),
            ImageId = Guid.Parse("B29B4E06-E84D-4BB2-B4D4-DC02725F8398"),
            UserName = "superadmin@gmail.com",
            NormalizedUserName = "SUPERADMIN@GMAIL.COM",
            Email = "superadmin@gmail.com",
            NormalizedEmail = "SUPERADMIN@GMAIL.COM",
            PhoneNumber = "+905555555555",
            FirstName = "Furkan",
            LastName = "Altıntaş",
            PhoneNumberConfirmed = true,
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        superAdmin.PasswordHash = CreatePasswordHash(superAdmin, "12345");

        // Admin
        var admin = new AppUser
        {
            Id = Guid.Parse("330ADE9E-AE19-4376-9B14-FDFC3F71FB4C"),
            ImageId = Guid.Parse("007D16D1-37D2-4400-943E-2452059151DE"),
            UserName = "admin@gmail.com",
            NormalizedUserName = "ADMIN@GMAIL.COM",
            Email = "admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            PhoneNumber = "+905555555556",
            FirstName = "Berke",
            LastName = "Altıntaş",
            PhoneNumberConfirmed = false,
            EmailConfirmed = false,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        admin.PasswordHash = CreatePasswordHash(admin, "123456");
        #endregion

        builder.HasData(superAdmin, admin);
    }

    private string CreatePasswordHash(AppUser user, string password)
        => new PasswordHasher<AppUser>().HashPassword(user, password);
}