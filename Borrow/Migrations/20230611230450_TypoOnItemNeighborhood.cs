using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrow.Migrations
{
    /// <inheritdoc />
    public partial class TypoOnItemNeighborhood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fda69022-637f-4a20-8885-ffe71600e2ef");

            migrationBuilder.RenameColumn(
                name: "Neighborhood",
                table: "Item",
                newName: "NeighborhoodId");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfileId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "bb4abced-678e-4644-a74e-982cba2a60aa", 0, "b648f949-6c46-437e-aff8-3b3de5fadf82", "test@tset.com", false, "Lazaro", "Hernandez", false, null, null, null, "1234567899", "2813308004", false, 0, "0c744a41-1148-4e31-a308-f762c3ae4874", false, "lazuhrow93" });

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 1,
                column: "Identifier",
                value: new Guid("70a8438f-4d71-4cba-bfbd-44e8a08ff704"));

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 2,
                column: "Identifier",
                value: new Guid("186cabbd-e336-43ff-9771-54666e7e586e"));

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 3,
                column: "Identifier",
                value: new Guid("12dce27c-08db-4ce4-b1ae-e45e001cbe84"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bb4abced-678e-4644-a74e-982cba2a60aa");

            migrationBuilder.RenameColumn(
                name: "NeighborhoodId",
                table: "Item",
                newName: "Neighborhood");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfileId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "fda69022-637f-4a20-8885-ffe71600e2ef", 0, "a53cfd3f-ace7-41de-bf6c-a0ad03b5aaa0", "test@tset.com", false, "Lazaro", "Hernandez", false, null, null, null, "1234567899", "2813308004", false, 0, "35bbccad-d3c3-4efd-a23e-980c1760cf6d", false, "lazuhrow93" });

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 1,
                column: "Identifier",
                value: new Guid("400fa183-06d1-4647-ac54-cd7a2e3833d6"));

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 2,
                column: "Identifier",
                value: new Guid("8abbb7f8-8bcc-4856-a678-aa051e4b526e"));

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 3,
                column: "Identifier",
                value: new Guid("345d1f65-143a-42d9-aae0-3d34284b642e"));
        }
    }
}
