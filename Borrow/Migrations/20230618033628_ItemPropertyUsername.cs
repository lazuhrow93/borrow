using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Borrow.Migrations
{
    /// <inheritdoc />
    public partial class ItemPropertyUsername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppProfile",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bb4abced-678e-4644-a74e-982cba2a60aa");

            migrationBuilder.DeleteData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Neighborhood",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Item",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Item");

            migrationBuilder.InsertData(
                table: "AppProfile",
                columns: new[] { "Id", "NeighborhoodId", "OwnerId" },
                values: new object[] { 1, 1, 2 });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfileId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "bb4abced-678e-4644-a74e-982cba2a60aa", 0, "b648f949-6c46-437e-aff8-3b3de5fadf82", "test@tset.com", false, "Lazaro", "Hernandez", false, null, null, null, "1234567899", "2813308004", false, 0, "0c744a41-1148-4e31-a308-f762c3ae4874", false, "lazuhrow93" });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "Id", "Available", "DailyRate", "Description", "Identifier", "IsListed", "Name", "NeighborhoodId", "OwnedSince", "OwnerId", "WeeklyRate" },
                values: new object[,]
                {
                    { 1, false, 10.00m, "Machine to mow lawns", new Guid("70a8438f-4d71-4cba-bfbd-44e8a08ff704"), false, "Lawn Mower", 0, new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0m },
                    { 2, true, 5.00m, "Machine to Trim and cut lawns", new Guid("186cabbd-e336-43ff-9771-54666e7e586e"), false, "Weed Eater", 0, new DateTime(2023, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0m },
                    { 3, false, 5.00m, "Machine to blow", new Guid("12dce27c-08db-4ce4-b1ae-e45e001cbe84"), false, "Leaf Blower", 0, new DateTime(2023, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0m }
                });

            migrationBuilder.InsertData(
                table: "Neighborhood",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Spring Lakes" });
        }
    }
}
