using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrow.Migrations
{
    /// <inheritdoc />
    public partial class ItemsNeighborhoodAssociation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d242c612-9e11-4de0-97e4-c48533d59acb");

            migrationBuilder.RenameColumn(
                name: "Neighborhood",
                table: "AppProfile",
                newName: "NeighborhoodId");

            migrationBuilder.AddColumn<int>(
                name: "Neighborhood",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfileId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "fda69022-637f-4a20-8885-ffe71600e2ef", 0, "a53cfd3f-ace7-41de-bf6c-a0ad03b5aaa0", "test@tset.com", false, "Lazaro", "Hernandez", false, null, null, null, "1234567899", "2813308004", false, 0, "35bbccad-d3c3-4efd-a23e-980c1760cf6d", false, "lazuhrow93" });

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Identifier", "Neighborhood" },
                values: new object[] { new Guid("400fa183-06d1-4647-ac54-cd7a2e3833d6"), 0 });

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Identifier", "Neighborhood" },
                values: new object[] { new Guid("8abbb7f8-8bcc-4856-a678-aa051e4b526e"), 0 });

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Identifier", "Neighborhood" },
                values: new object[] { new Guid("345d1f65-143a-42d9-aae0-3d34284b642e"), 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fda69022-637f-4a20-8885-ffe71600e2ef");

            migrationBuilder.DropColumn(
                name: "Neighborhood",
                table: "Item");

            migrationBuilder.RenameColumn(
                name: "NeighborhoodId",
                table: "AppProfile",
                newName: "Neighborhood");

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
    }
}
