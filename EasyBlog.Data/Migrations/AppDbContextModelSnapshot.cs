﻿// <auto-generated />
using System;
using EasyBlog.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EasyBlog.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EasyBlog.Entity.Entities.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("d67d8f29-6ff0-4e7b-b582-c51b34618674"),
                            ConcurrencyStamp = "7479bcba-d28a-430a-a714-00c98532c15a",
                            Name = "SuperAdmin",
                            NormalizedName = "SUPERADMIN"
                        },
                        new
                        {
                            Id = new Guid("40a173ea-1326-41eb-927a-3729c7277be7"),
                            ConcurrencyStamp = "16018b51-7f69-4e19-9096-27b6f2355ab1",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("7e35da39-e96c-4113-8d4b-4f7fed9d8ebb"),
                            ConcurrencyStamp = "746f2c4a-930a-4f1d-a0ea-48a72ac20c8a",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.AppRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("4fcc7985-f39b-4c50-ad1c-ade5d0df8279"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "891ee386-98cf-418a-b24f-e50d66050428",
                            Email = "superadmin@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Furkan",
                            ImageId = new Guid("b29b4e06-e84d-4bb2-b4d4-dc02725f8398"),
                            LastName = "Altıntaş",
                            LockoutEnabled = false,
                            NormalizedEmail = "SUPERADMIN@GMAIL.COM",
                            NormalizedUserName = "SUPERADMIN@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEDCGbZZt0GKwW1bVkSDpyEqE6dmvUtbGT61h1Gue7HsuPIRl+F+17PYhJ5+LnVg9hg==",
                            PhoneNumber = "+905555555555",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "9c8dfe68-ae4d-478d-af1a-3ba31000c378",
                            TwoFactorEnabled = false,
                            UserName = "superadmin@gmail.com"
                        },
                        new
                        {
                            Id = new Guid("330ade9e-ae19-4376-9b14-fdfc3f71fb4c"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "00707e03-23ee-4789-8117-c24e86f3be84",
                            Email = "admin@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Berke",
                            ImageId = new Guid("007d16d1-37d2-4400-943e-2452059151de"),
                            LastName = "Altıntaş",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            NormalizedUserName = "ADMIN@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEJTy4gI/40y/B4Ml8UtOuP3h3Ed7ghTNLX6zNzvjtDUdWqa5Sp6n1GkXjer6tG1eVw==",
                            PhoneNumber = "+905555555556",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "09fb6635-8025-49f4-acd1-d8b0fb513ad4",
                            TwoFactorEnabled = false,
                            UserName = "admin@gmail.com"
                        });
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.AppUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.AppUserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.AppUserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("4fcc7985-f39b-4c50-ad1c-ade5d0df8279"),
                            RoleId = new Guid("d67d8f29-6ff0-4e7b-b582-c51b34618674")
                        },
                        new
                        {
                            UserId = new Guid("330ade9e-ae19-4376-9b14-fdfc3f71fb4c"),
                            RoleId = new Guid("40a173ea-1326-41eb-927a-3729c7277be7")
                        });
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.AppUserToken", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ViewCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ImageId");

                    b.HasIndex("UserId");

                    b.ToTable("Articles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("816101ce-ce06-4ac1-848b-3ebc89e2522f"),
                            CategoryId = new Guid("58456306-9248-4cd3-b0aa-f7c9c53c5d5e"),
                            Content = "Lorem Ipsum 1 is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                            CreatedBy = "Admin User",
                            CreatedDate = new DateTime(2025, 3, 4, 17, 21, 51, 596, DateTimeKind.Utc).AddTicks(4292),
                            DeletedBy = "",
                            ImageId = new Guid("b29b4e06-e84d-4bb2-b4d4-dc02725f8398"),
                            IsDeleted = false,
                            ModifiedBy = "",
                            Title = "Lorem Ipsum 1",
                            UserId = new Guid("4fcc7985-f39b-4c50-ad1c-ade5d0df8279"),
                            ViewCount = 15
                        },
                        new
                        {
                            Id = new Guid("adb2a019-fc0d-4fe6-92a8-eee6b3f84d97"),
                            CategoryId = new Guid("d0a592ee-589e-4d43-a83d-0e60dc239368"),
                            Content = "Lorem Ipsum 2 is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                            CreatedBy = "Admin User",
                            CreatedDate = new DateTime(2025, 3, 4, 17, 21, 51, 597, DateTimeKind.Utc).AddTicks(60),
                            DeletedBy = "",
                            ImageId = new Guid("007d16d1-37d2-4400-943e-2452059151de"),
                            IsDeleted = false,
                            ModifiedBy = "",
                            Title = "Lorem Ipsum 2",
                            UserId = new Guid("330ade9e-ae19-4376-9b14-fdfc3f71fb4c"),
                            ViewCount = 15
                        });
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("58456306-9248-4cd3-b0aa-f7c9c53c5d5e"),
                            CreatedBy = "Admin Test",
                            CreatedDate = new DateTime(2025, 3, 4, 17, 21, 51, 598, DateTimeKind.Utc).AddTicks(2145),
                            DeletedBy = "",
                            IsDeleted = false,
                            ModifiedBy = "",
                            Name = "Lorem Ipsum Category 1"
                        },
                        new
                        {
                            Id = new Guid("d0a592ee-589e-4d43-a83d-0e60dc239368"),
                            CreatedBy = "Admin Test",
                            CreatedDate = new DateTime(2025, 3, 4, 17, 21, 51, 598, DateTimeKind.Utc).AddTicks(6071),
                            DeletedBy = "",
                            IsDeleted = false,
                            ModifiedBy = "",
                            Name = "Lorem Ipsum Category 2"
                        });
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Images");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b29b4e06-e84d-4bb2-b4d4-dc02725f8398"),
                            CreatedBy = "Admin User",
                            CreatedDate = new DateTime(2025, 3, 4, 17, 21, 51, 600, DateTimeKind.Utc).AddTicks(57),
                            DeletedBy = "",
                            FileName = "images/test1",
                            FileType = "jpg",
                            IsDeleted = false,
                            ModifiedBy = ""
                        },
                        new
                        {
                            Id = new Guid("007d16d1-37d2-4400-943e-2452059151de"),
                            CreatedBy = "Admin User",
                            CreatedDate = new DateTime(2025, 3, 4, 17, 21, 51, 600, DateTimeKind.Utc).AddTicks(4591),
                            DeletedBy = "",
                            FileName = "images/test2",
                            FileType = "jpeg",
                            IsDeleted = false,
                            ModifiedBy = ""
                        });
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.AppRoleClaim", b =>
                {
                    b.HasOne("EasyBlog.Entity.Entities.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.AppUser", b =>
                {
                    b.HasOne("EasyBlog.Entity.Entities.Image", "Image")
                        .WithMany("Users")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Image");
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.AppUserClaim", b =>
                {
                    b.HasOne("EasyBlog.Entity.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.AppUserLogin", b =>
                {
                    b.HasOne("EasyBlog.Entity.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.AppUserRole", b =>
                {
                    b.HasOne("EasyBlog.Entity.Entities.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyBlog.Entity.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.AppUserToken", b =>
                {
                    b.HasOne("EasyBlog.Entity.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.Article", b =>
                {
                    b.HasOne("EasyBlog.Entity.Entities.Category", "Category")
                        .WithMany("Articles")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyBlog.Entity.Entities.Image", "Image")
                        .WithMany("Articles")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("EasyBlog.Entity.Entities.AppUser", "User")
                        .WithMany("Articles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Image");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.AppUser", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.Category", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.Image", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
