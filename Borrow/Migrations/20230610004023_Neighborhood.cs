using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrow.Migrations
{
    /// <inheritdoc />
    public partial class Neighborhood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8fe4d2c3-df0a-4a55-8c1e-94dae64ba4e3");

            migrationBuilder.CreateTable(
                name: "Neighborhood",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Neighborhood", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OwnerId", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "80343a2d-d046-4e85-abbb-fb5b0ecc9666", 0, "df0f76d8-72ff-4510-b50a-de2057ff067c", "test@tset.com", false, "Lazaro", "Hernandez", false, null, null, null, 2, "1234567899", "2813308004", false, "3513731b-c057-4a14-8123-12788b0fed4b", false, "lazuhrow93" });

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 1,
                column: "Identifier",
                value: new Guid("eb0cb738-d317-437a-901e-84ccecd1f7a1"));

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 2,
                column: "Identifier",
                value: new Guid("1d7b400a-e4eb-46b3-af04-e4978ac2aad1"));

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 3,
                column: "Identifier",
                value: new Guid("a43ac9a6-93f3-4429-9e2e-05f8cd82d11c"));

            migrationBuilder.InsertData(
                table: "Neighborhood",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Spring Lakes" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Neighborhood");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "80343a2d-d046-4e85-abbb-fb5b0ecc9666");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OwnerId", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8fe4d2c3-df0a-4a55-8c1e-94dae64ba4e3", 0, "9a0878c5-1a43-4094-8c7e-543bfc5ea921", "test@tset.com", false, "Lazaro", "Hernandez", false, null, null, null, 2, "1234567899", "2813308004", false, "3c968305-794f-4460-b567-61a7246fc2e2", false, "lazuhrow93" });

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 1,
                column: "Identifier",
                value: new Guid("f8e6e29b-37e7-4d30-9a1c-d9d5dd57b596"));

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 2,
                column: "Identifier",
                value: new Guid("2a3f16f4-605b-4d43-b222-12a2fddf76bc"));

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 3,
                column: "Identifier",
                value: new Guid("effc3bcb-89f0-4cc3-b45c-88550972b729"));
        }
    }
}
