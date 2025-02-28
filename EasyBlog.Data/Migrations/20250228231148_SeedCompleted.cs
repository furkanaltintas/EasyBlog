using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EasyBlog.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedCompleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FileType",
                table: "Images",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Images",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Articles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Articles",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("58456306-9248-4cd3-b0aa-f7c9c53c5d5e"), "Admin Test", new DateTime(2025, 2, 28, 23, 11, 48, 389, DateTimeKind.Utc).AddTicks(1863), "", null, false, "", null, "Lorem Ipsum Category 1" },
                    { new Guid("d0a592ee-589e-4d43-a83d-0e60dc239368"), "Admin Test", new DateTime(2025, 2, 28, 23, 11, 48, 389, DateTimeKind.Utc).AddTicks(8680), "", null, false, "", null, "Lorem Ipsum Category 2" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "FileName", "FileType", "IsDeleted", "ModifiedBy", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("007d16d1-37d2-4400-943e-2452059151de"), "Admin User", new DateTime(2025, 2, 28, 23, 11, 48, 392, DateTimeKind.Utc).AddTicks(251), "", null, "images/test2", "jpeg", false, "", null },
                    { new Guid("b29b4e06-e84d-4bb2-b4d4-dc02725f8398"), "Admin User", new DateTime(2025, 2, 28, 23, 11, 48, 391, DateTimeKind.Utc).AddTicks(5198), "", null, "images/test1", "jpg", false, "", null }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "ImageId", "IsDeleted", "ModifiedBy", "ModifiedDate", "Title", "ViewCount" },
                values: new object[,]
                {
                    { new Guid("a608ac09-0bf2-4b05-a62d-816accc4f2b7"), new Guid("58456306-9248-4cd3-b0aa-f7c9c53c5d5e"), "Lorem Ipsum 1 is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.", "Admin User", new DateTime(2025, 2, 28, 23, 11, 48, 392, DateTimeKind.Utc).AddTicks(8130), "", null, new Guid("b29b4e06-e84d-4bb2-b4d4-dc02725f8398"), false, "", null, "Lorem Ipsum 1", 15 },
                    { new Guid("bd9c0169-e8c1-4262-960c-e6ddc74b875a"), new Guid("d0a592ee-589e-4d43-a83d-0e60dc239368"), "Lorem Ipsum 2 is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.", "Admin User", new DateTime(2025, 2, 28, 23, 11, 48, 393, DateTimeKind.Utc).AddTicks(2542), "", null, new Guid("007d16d1-37d2-4400-943e-2452059151de"), false, "", null, "Lorem Ipsum 2", 15 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("a608ac09-0bf2-4b05-a62d-816accc4f2b7"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("bd9c0169-e8c1-4262-960c-e6ddc74b875a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("58456306-9248-4cd3-b0aa-f7c9c53c5d5e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d0a592ee-589e-4d43-a83d-0e60dc239368"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("007d16d1-37d2-4400-943e-2452059151de"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("b29b4e06-e84d-4bb2-b4d4-dc02725f8398"));

            migrationBuilder.AlterColumn<string>(
                name: "FileType",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);
        }
    }
}
