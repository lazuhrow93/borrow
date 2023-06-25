using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrow.Migrations
{
    /// <inheritdoc />
    public partial class Newvaluesforexistingrecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppProfile",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequestKey",
                value: new Guid("873f192b-1861-4209-80d8-9798fa88f6e2"));

            migrationBuilder.UpdateData(
                table: "AppProfile",
                keyColumn: "Id",
                keyValue: 1005,
                column: "RequestKey",
                value: new Guid("0afff249-8ce9-4209-94e8-d3df23637dcf"));

            migrationBuilder.UpdateData(
                table: "AppProfile",
                keyColumn: "Id",
                keyValue: 1006,
                column: "RequestKey",
                value: new Guid("0afff249-8ce9-4209-94e8-d3df236dd98f"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppProfile",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequestKey",
                value: new Guid("0afff249-8ce9-4af6-94e8-d3e477637dcf"));
        }
    }
}
