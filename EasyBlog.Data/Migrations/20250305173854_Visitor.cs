using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EasyBlog.Data.Migrations
{
    /// <inheritdoc />
    public partial class Visitor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("816101ce-ce06-4ac1-848b-3ebc89e2522f"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("adb2a019-fc0d-4fe6-92a8-eee6b3f84d97"));

            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleVisitor",
                columns: table => new
                {
                    ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleVisitor", x => new { x.ArticleId, x.VisitorId });
                    table.ForeignKey(
                        name: "FK_ArticleVisitor_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleVisitor_Visitors_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Visitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "ImageId", "IsDeleted", "ModifiedBy", "ModifiedDate", "Title", "UserId", "ViewCount" },
                values: new object[,]
                {
                    { new Guid("0fe6339d-7c14-44f8-ba82-3b09fd159eac"), new Guid("58456306-9248-4cd3-b0aa-f7c9c53c5d5e"), "Lorem Ipsum 1 is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.", "Admin User", new DateTime(2025, 3, 5, 17, 38, 52, 702, DateTimeKind.Utc).AddTicks(2672), "", null, new Guid("b29b4e06-e84d-4bb2-b4d4-dc02725f8398"), false, "", null, "Lorem Ipsum 1", new Guid("4fcc7985-f39b-4c50-ad1c-ade5d0df8279"), 15 },
                    { new Guid("7b64152c-b111-4f38-8388-2b6cec9b1c9d"), new Guid("d0a592ee-589e-4d43-a83d-0e60dc239368"), "Lorem Ipsum 2 is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.", "Admin User", new DateTime(2025, 3, 5, 17, 38, 52, 702, DateTimeKind.Utc).AddTicks(7924), "", null, new Guid("007d16d1-37d2-4400-943e-2452059151de"), false, "", null, "Lorem Ipsum 2", new Guid("330ade9e-ae19-4376-9b14-fdfc3f71fb4c"), 15 }
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("40a173ea-1326-41eb-927a-3729c7277be7"),
                column: "ConcurrencyStamp",
                value: "a8935625-7960-4228-a5f5-944578e5ce4a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7e35da39-e96c-4113-8d4b-4f7fed9d8ebb"),
                column: "ConcurrencyStamp",
                value: "1ac7c5f1-3bd1-448c-aed1-89185ef184a5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d67d8f29-6ff0-4e7b-b582-c51b34618674"),
                column: "ConcurrencyStamp",
                value: "fb07d654-39d0-4b9a-a321-6eb87b41a74a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("330ade9e-ae19-4376-9b14-fdfc3f71fb4c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "15026269-f496-4e7b-9ff6-c46688b64079", "AQAAAAIAAYagAAAAEDPEFiFLIFk3lsl35sDllchd7lpgOfDZAK150hvjrsgARRRl45mpbkUv53NdGv0Llg==", "ae9f52ac-4636-482f-8e8a-1c0f98a01f64" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4fcc7985-f39b-4c50-ad1c-ade5d0df8279"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab4d09a1-1880-4694-aeb3-de94b707a3d9", "AQAAAAIAAYagAAAAEBMvP0kSQrZh7UdwhA/vssYxBOoConwfh9ieYfj4DAY6HMkltHSccvidFy4jdzQ4Lg==", "c86f5153-5e26-4098-99b9-ff95714abfa2" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("58456306-9248-4cd3-b0aa-f7c9c53c5d5e"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 5, 17, 38, 52, 704, DateTimeKind.Utc).AddTicks(3825));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d0a592ee-589e-4d43-a83d-0e60dc239368"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 5, 17, 38, 52, 704, DateTimeKind.Utc).AddTicks(7970));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("007d16d1-37d2-4400-943e-2452059151de"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 5, 17, 38, 52, 706, DateTimeKind.Utc).AddTicks(7030));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("b29b4e06-e84d-4bb2-b4d4-dc02725f8398"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 5, 17, 38, 52, 706, DateTimeKind.Utc).AddTicks(2549));

            migrationBuilder.CreateIndex(
                name: "IX_ArticleVisitor_VisitorId",
                table: "ArticleVisitor",
                column: "VisitorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleVisitor");

            migrationBuilder.DropTable(
                name: "Visitors");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("0fe6339d-7c14-44f8-ba82-3b09fd159eac"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("7b64152c-b111-4f38-8388-2b6cec9b1c9d"));

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "ImageId", "IsDeleted", "ModifiedBy", "ModifiedDate", "Title", "UserId", "ViewCount" },
                values: new object[,]
                {
                    { new Guid("816101ce-ce06-4ac1-848b-3ebc89e2522f"), new Guid("58456306-9248-4cd3-b0aa-f7c9c53c5d5e"), "Lorem Ipsum 1 is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.", "Admin User", new DateTime(2025, 3, 4, 17, 21, 51, 596, DateTimeKind.Utc).AddTicks(4292), "", null, new Guid("b29b4e06-e84d-4bb2-b4d4-dc02725f8398"), false, "", null, "Lorem Ipsum 1", new Guid("4fcc7985-f39b-4c50-ad1c-ade5d0df8279"), 15 },
                    { new Guid("adb2a019-fc0d-4fe6-92a8-eee6b3f84d97"), new Guid("d0a592ee-589e-4d43-a83d-0e60dc239368"), "Lorem Ipsum 2 is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.", "Admin User", new DateTime(2025, 3, 4, 17, 21, 51, 597, DateTimeKind.Utc).AddTicks(60), "", null, new Guid("007d16d1-37d2-4400-943e-2452059151de"), false, "", null, "Lorem Ipsum 2", new Guid("330ade9e-ae19-4376-9b14-fdfc3f71fb4c"), 15 }
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("40a173ea-1326-41eb-927a-3729c7277be7"),
                column: "ConcurrencyStamp",
                value: "16018b51-7f69-4e19-9096-27b6f2355ab1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7e35da39-e96c-4113-8d4b-4f7fed9d8ebb"),
                column: "ConcurrencyStamp",
                value: "746f2c4a-930a-4f1d-a0ea-48a72ac20c8a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d67d8f29-6ff0-4e7b-b582-c51b34618674"),
                column: "ConcurrencyStamp",
                value: "7479bcba-d28a-430a-a714-00c98532c15a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("330ade9e-ae19-4376-9b14-fdfc3f71fb4c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "00707e03-23ee-4789-8117-c24e86f3be84", "AQAAAAIAAYagAAAAEJTy4gI/40y/B4Ml8UtOuP3h3Ed7ghTNLX6zNzvjtDUdWqa5Sp6n1GkXjer6tG1eVw==", "09fb6635-8025-49f4-acd1-d8b0fb513ad4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4fcc7985-f39b-4c50-ad1c-ade5d0df8279"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "891ee386-98cf-418a-b24f-e50d66050428", "AQAAAAIAAYagAAAAEDCGbZZt0GKwW1bVkSDpyEqE6dmvUtbGT61h1Gue7HsuPIRl+F+17PYhJ5+LnVg9hg==", "9c8dfe68-ae4d-478d-af1a-3ba31000c378" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("58456306-9248-4cd3-b0aa-f7c9c53c5d5e"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 4, 17, 21, 51, 598, DateTimeKind.Utc).AddTicks(2145));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d0a592ee-589e-4d43-a83d-0e60dc239368"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 4, 17, 21, 51, 598, DateTimeKind.Utc).AddTicks(6071));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("007d16d1-37d2-4400-943e-2452059151de"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 4, 17, 21, 51, 600, DateTimeKind.Utc).AddTicks(4591));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("b29b4e06-e84d-4bb2-b4d4-dc02725f8398"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 4, 17, 21, 51, 600, DateTimeKind.Utc).AddTicks(57));
        }
    }
}
