using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrow.Migrations
{
    /// <inheritdoc />
    public partial class UserItemLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f17964ed-1a91-46cd-a27c-a4738eeedc63");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3cbc2359-bd33-4f98-b191-61668d237796", 0, "91800456-22e8-46a1-81c2-8fcc1d8fb1cb", "test@tset.com", false, "Lazaro", "Hernandez", false, null, null, null, "1234567899", "2813308004", false, "094967a0-dabb-4e5f-8ace-18c59f430b81", false, "lazuhrow93" });

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 1,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 2,
                column: "OwnerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 3,
                column: "OwnerId",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3cbc2359-bd33-4f98-b191-61668d237796");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Item");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f17964ed-1a91-46cd-a27c-a4738eeedc63", 0, "e162d459-c0f2-4f95-8fd5-2d25bf4df8a2", "test@tset.com", false, "Lazaro", "Hernandez", false, null, null, null, "1234567899", "2813308004", false, "40a40b28-fde0-49a4-894c-50514ea565ec", false, "lazuhrow93" });
        }
    }
}
