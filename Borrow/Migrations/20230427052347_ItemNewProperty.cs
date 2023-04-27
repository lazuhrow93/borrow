using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrow.Migrations
{
    /// <inheritdoc />
    public partial class ItemNewProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6db418fa-727f-4df6-99ba-8c674f58d191");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Item");

            migrationBuilder.AddColumn<DateTime>(
                name: "OwnedSince",
                table: "Item",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OwnerId", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b401780b-7804-457d-a9f1-080d8c7c9e6c", 0, "a257aff2-fb5f-4ea9-8ef1-05005cc047e6", "test@tset.com", false, "Lazaro", "Hernandez", false, null, null, null, 2, "1234567899", "2813308004", false, "50d4c3f6-918e-411c-b538-6c4d1d07de34", false, "lazuhrow93" });

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 1,
                column: "OwnedSince",
                value: new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 2,
                column: "OwnedSince",
                value: new DateTime(2023, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 3,
                column: "OwnedSince",
                value: new DateTime(2023, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b401780b-7804-457d-a9f1-080d8c7c9e6c");

            migrationBuilder.DropColumn(
                name: "OwnedSince",
                table: "Item");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Age",
                table: "Item",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OwnerId", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6db418fa-727f-4df6-99ba-8c674f58d191", 0, "bf133c78-99c0-4f34-afcb-12138d479d16", "test@tset.com", false, "Lazaro", "Hernandez", false, null, null, null, 2, "1234567899", "2813308004", false, "f42f3371-d8f9-42bf-aef1-5b79a8c18d19", false, "lazuhrow93" });

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 1,
                column: "Age",
                value: new TimeSpan(1, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 2,
                column: "Age",
                value: new TimeSpan(1, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 3,
                column: "Age",
                value: new TimeSpan(1, 0, 0, 0, 0));
        }
    }
}
