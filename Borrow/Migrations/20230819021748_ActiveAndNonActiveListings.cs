using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrow.Migrations
{
    /// <inheritdoc />
    public partial class ActiveAndNonActiveListings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Listing",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AppProfile",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequestKey",
                value: new Guid("ea4f73b2-f731-48ae-9903-17b0b73e3e92"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Listing");

            migrationBuilder.UpdateData(
                table: "AppProfile",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequestKey",
                value: new Guid("64e8bf5e-2196-457a-b8d9-a273a963ca16"));
        }
    }
}
