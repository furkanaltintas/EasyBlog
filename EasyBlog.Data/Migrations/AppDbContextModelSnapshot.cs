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
                            ConcurrencyStamp = "fb07d654-39d0-4b9a-a321-6eb87b41a74a",
                            Name = "SuperAdmin",
                            NormalizedName = "SUPERADMIN"
                        },
                        new
                        {
                            Id = new Guid("40a173ea-1326-41eb-927a-3729c7277be7"),
                            ConcurrencyStamp = "a8935625-7960-4228-a5f5-944578e5ce4a",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("7e35da39-e96c-4113-8d4b-4f7fed9d8ebb"),
                            ConcurrencyStamp = "1ac7c5f1-3bd1-448c-aed1-89185ef184a5",
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
                            ConcurrencyStamp = "ab4d09a1-1880-4694-aeb3-de94b707a3d9",
                            Email = "superadmin@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Furkan",
                            ImageId = new Guid("b29b4e06-e84d-4bb2-b4d4-dc02725f8398"),
                            LastName = "Altıntaş",
                            LockoutEnabled = false,
                            NormalizedEmail = "SUPERADMIN@GMAIL.COM",
                            NormalizedUserName = "SUPERADMIN@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEBMvP0kSQrZh7UdwhA/vssYxBOoConwfh9ieYfj4DAY6HMkltHSccvidFy4jdzQ4Lg==",
                            PhoneNumber = "+905555555555",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "c86f5153-5e26-4098-99b9-ff95714abfa2",
                            TwoFactorEnabled = false,
                            UserName = "superadmin@gmail.com"
                        },
                        new
                        {
                            Id = new Guid("330ade9e-ae19-4376-9b14-fdfc3f71fb4c"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "15026269-f496-4e7b-9ff6-c46688b64079",
                            Email = "admin@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Berke",
                            ImageId = new Guid("007d16d1-37d2-4400-943e-2452059151de"),
                            LastName = "Altıntaş",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            NormalizedUserName = "ADMIN@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEDPEFiFLIFk3lsl35sDllchd7lpgOfDZAK150hvjrsgARRRl45mpbkUv53NdGv0Llg==",
                            PhoneNumber = "+905555555556",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "ae9f52ac-4636-482f-8e8a-1c0f98a01f64",
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
                            Id = new Guid("0fe6339d-7c14-44f8-ba82-3b09fd159eac"),
                            CategoryId = new Guid("58456306-9248-4cd3-b0aa-f7c9c53c5d5e"),
                            Content = "Lorem Ipsum 1 is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                            CreatedBy = "Admin User",
                            CreatedDate = new DateTime(2025, 3, 5, 17, 38, 52, 702, DateTimeKind.Utc).AddTicks(2672),
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
                            Id = new Guid("7b64152c-b111-4f38-8388-2b6cec9b1c9d"),
                            CategoryId = new Guid("d0a592ee-589e-4d43-a83d-0e60dc239368"),
                            Content = "Lorem Ipsum 2 is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                            CreatedBy = "Admin User",
                            CreatedDate = new DateTime(2025, 3, 5, 17, 38, 52, 702, DateTimeKind.Utc).AddTicks(7924),
                            DeletedBy = "",
                            ImageId = new Guid("007d16d1-37d2-4400-943e-2452059151de"),
                            IsDeleted = false,
                            ModifiedBy = "",
                            Title = "Lorem Ipsum 2",
                            UserId = new Guid("330ade9e-ae19-4376-9b14-fdfc3f71fb4c"),
                            ViewCount = 15
                        });
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.ArticleVisitor", b =>
                {
                    b.Property<Guid>("ArticleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VisitorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ArticleId", "VisitorId");

                    b.HasIndex("VisitorId");

                    b.ToTable("ArticleVisitor");
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
                            CreatedDate = new DateTime(2025, 3, 5, 17, 38, 52, 704, DateTimeKind.Utc).AddTicks(3825),
                            DeletedBy = "",
                            IsDeleted = false,
                            ModifiedBy = "",
                            Name = "Lorem Ipsum Category 1"
                        },
                        new
                        {
                            Id = new Guid("d0a592ee-589e-4d43-a83d-0e60dc239368"),
                            CreatedBy = "Admin Test",
                            CreatedDate = new DateTime(2025, 3, 5, 17, 38, 52, 704, DateTimeKind.Utc).AddTicks(7970),
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
                            CreatedDate = new DateTime(2025, 3, 5, 17, 38, 52, 706, DateTimeKind.Utc).AddTicks(2549),
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
                            CreatedDate = new DateTime(2025, 3, 5, 17, 38, 52, 706, DateTimeKind.Utc).AddTicks(7030),
                            DeletedBy = "",
                            FileName = "images/test2",
                            FileType = "jpeg",
                            IsDeleted = false,
                            ModifiedBy = ""
                        });
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.Visitor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserAgent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Visitors");
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

            modelBuilder.Entity("EasyBlog.Entity.Entities.ArticleVisitor", b =>
                {
                    b.HasOne("EasyBlog.Entity.Entities.Article", "Article")
                        .WithMany("ArticleVisitors")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyBlog.Entity.Entities.Visitor", "Visitor")
                        .WithMany("ArticleVisitors")
                        .HasForeignKey("VisitorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("Visitor");
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.AppUser", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.Article", b =>
                {
                    b.Navigation("ArticleVisitors");
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

            modelBuilder.Entity("EasyBlog.Entity.Entities.Visitor", b =>
                {
                    b.Navigation("ArticleVisitors");
                });
#pragma warning restore 612, 618
        }
    }
}
