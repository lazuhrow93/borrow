using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrow.Migrations
{
    /// <inheritdoc />
    public partial class RequestTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequesterProfileId",
                table: "BorrowRequests",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "OwnerProfileId",
                table: "BorrowRequests",
                newName: "RequesterOwnerId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "BorrowRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BorrowRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "BorrowRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "BorrowRequests");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "BorrowRequests",
                newName: "RequesterProfileId");

            migrationBuilder.RenameColumn(
                name: "RequesterOwnerId",
                table: "BorrowRequests",
                newName: "OwnerProfileId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "BorrowRequests",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BorrowRequests",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
