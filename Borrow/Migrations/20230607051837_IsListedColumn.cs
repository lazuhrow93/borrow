using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrow.Migrations
{
    /// <inheritdoc />
    public partial class IsListedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d4d85115-bebe-4dac-950e-a65dfc81ba4f");

            migrationBuilder.AddColumn<bool>(
                name: "IsListed",
                table: "Item",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OwnerId", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8fe4d2c3-df0a-4a55-8c1e-94dae64ba4e3", 0, "9a0878c5-1a43-4094-8c7e-543bfc5ea921", "test@tset.com", false, "Lazaro", "Hernandez", false, null, null, null, 2, "1234567899", "2813308004", false, "3c968305-794f-4460-b567-61a7246fc2e2", false, "lazuhrow93" });

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Identifier", "IsListed" },
                values: new object[] { new Guid("f8e6e29b-37e7-4d30-9a1c-d9d5dd57b596"), false });

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Identifier", "IsListed" },
                values: new object[] { new Guid("2a3f16f4-605b-4d43-b222-12a2fddf76bc"), false });

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Identifier", "IsListed" },
                values: new object[] { new Guid("effc3bcb-89f0-4cc3-b45c-88550972b729"), false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8fe4d2c3-df0a-4a55-8c1e-94dae64ba4e3");

            migrationBuilder.DropColumn(
                name: "IsListed",
                table: "Item");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OwnerId", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d4d85115-bebe-4dac-950e-a65dfc81ba4f", 0, "53851b62-fa1f-4cdf-865f-4d1479587309", "test@tset.com", false, "Lazaro", "Hernandez", false, null, null, null, 2, "1234567899", "2813308004", false, "be326651-d71e-44ac-8a2d-d35e661b7fe8", false, "lazuhrow93" });

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 1,
                column: "Identifier",
                value: new Guid("967710d2-a78a-4007-a7a0-b4dd108173c4"));

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 2,
                column: "Identifier",
                value: new Guid("6f40aa15-c634-410a-bc3f-decdef5a47be"));

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 3,
                column: "Identifier",
                value: new Guid("d8c4fb0c-4e48-4068-8fb8-a8904f0e1eb3"));
        }
    }
}
