using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrow.Migrations
{
    /// <inheritdoc />
    public partial class AddingProfileTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "80343a2d-d046-4e85-abbb-fb5b0ecc9666");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "AspNetUsers",
                newName: "ProfileId");

            migrationBuilder.CreateTable(
                name: "AppProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    Neighborhood = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProfile", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AppProfile",
                columns: new[] { "Id", "Neighborhood", "OwnerId" },
                values: new object[] { 1, 1, 2 });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfileId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d242c612-9e11-4de0-97e4-c48533d59acb", 0, "2f41b8f2-0606-4558-9398-a228eeac0abb", "test@tset.com", false, "Lazaro", "Hernandez", false, null, null, null, "1234567899", "2813308004", false, 0, "6b699d74-9558-4385-a087-d7dbc15c16c1", false, "lazuhrow93" });

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 1,
                column: "Identifier",
                value: new Guid("7aed5f95-50ee-4e65-9e66-f8419983cce7"));

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 2,
                column: "Identifier",
                value: new Guid("b58ffd3a-3235-4eb6-b7a1-54e3b62a06be"));

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 3,
                column: "Identifier",
                value: new Guid("86025a07-5443-4d1f-becc-4959b4454df9"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppProfile");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d242c612-9e11-4de0-97e4-c48533d59acb");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "AspNetUsers",
                newName: "OwnerId");

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
        }
    }
}
