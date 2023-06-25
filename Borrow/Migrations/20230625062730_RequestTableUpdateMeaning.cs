using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrow.Migrations
{
    /// <inheritdoc />
    public partial class RequestTableUpdateMeaning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "BorrowRequests");

            migrationBuilder.DropColumn(
                name: "RequesterOwnerId",
                table: "BorrowRequests");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerKey",
                table: "BorrowRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RequesterKey",
                table: "BorrowRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RequestKey",
                table: "AppProfile",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AppProfile",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequestKey",
                value: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerKey",
                table: "BorrowRequests");

            migrationBuilder.DropColumn(
                name: "RequesterKey",
                table: "BorrowRequests");

            migrationBuilder.DropColumn(
                name: "RequestKey",
                table: "AppProfile");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "BorrowRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequesterOwnerId",
                table: "BorrowRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
