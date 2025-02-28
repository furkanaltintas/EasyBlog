﻿// <auto-generated />
using System;
using EasyBlog.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EasyBlog.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250228231148_SeedCompleted")]
    partial class SeedCompleted
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EasyBlog.Entity.Entities.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ViewCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ImageId");

                    b.ToTable("Articles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a608ac09-0bf2-4b05-a62d-816accc4f2b7"),
                            CategoryId = new Guid("58456306-9248-4cd3-b0aa-f7c9c53c5d5e"),
                            Content = "Lorem Ipsum 1 is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                            CreatedBy = "Admin User",
                            CreatedDate = new DateTime(2025, 2, 28, 23, 11, 48, 392, DateTimeKind.Utc).AddTicks(8130),
                            DeletedBy = "",
                            ImageId = new Guid("b29b4e06-e84d-4bb2-b4d4-dc02725f8398"),
                            IsDeleted = false,
                            ModifiedBy = "",
                            Title = "Lorem Ipsum 1",
                            ViewCount = 15
                        },
                        new
                        {
                            Id = new Guid("bd9c0169-e8c1-4262-960c-e6ddc74b875a"),
                            CategoryId = new Guid("d0a592ee-589e-4d43-a83d-0e60dc239368"),
                            Content = "Lorem Ipsum 2 is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                            CreatedBy = "Admin User",
                            CreatedDate = new DateTime(2025, 2, 28, 23, 11, 48, 393, DateTimeKind.Utc).AddTicks(2542),
                            DeletedBy = "",
                            ImageId = new Guid("007d16d1-37d2-4400-943e-2452059151de"),
                            IsDeleted = false,
                            ModifiedBy = "",
                            Title = "Lorem Ipsum 2",
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
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("58456306-9248-4cd3-b0aa-f7c9c53c5d5e"),
                            CreatedBy = "Admin Test",
                            CreatedDate = new DateTime(2025, 2, 28, 23, 11, 48, 389, DateTimeKind.Utc).AddTicks(1863),
                            DeletedBy = "",
                            IsDeleted = false,
                            ModifiedBy = "",
                            Name = "Lorem Ipsum Category 1"
                        },
                        new
                        {
                            Id = new Guid("d0a592ee-589e-4d43-a83d-0e60dc239368"),
                            CreatedBy = "Admin Test",
                            CreatedDate = new DateTime(2025, 2, 28, 23, 11, 48, 389, DateTimeKind.Utc).AddTicks(8680),
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
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

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
                            CreatedDate = new DateTime(2025, 2, 28, 23, 11, 48, 391, DateTimeKind.Utc).AddTicks(5198),
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
                            CreatedDate = new DateTime(2025, 2, 28, 23, 11, 48, 392, DateTimeKind.Utc).AddTicks(251),
                            DeletedBy = "",
                            FileName = "images/test2",
                            FileType = "jpeg",
                            IsDeleted = false,
                            ModifiedBy = ""
                        });
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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.Category", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("EasyBlog.Entity.Entities.Image", b =>
                {
                    b.Navigation("Articles");
                });
#pragma warning restore 612, 618
        }
    }
}
