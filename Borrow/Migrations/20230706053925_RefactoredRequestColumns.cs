using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrow.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredRequestColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Request",
                newName: "UpdateDateUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Request",
                newName: "CreatedDateUtc");

            migrationBuilder.AddColumn<decimal>(
                name: "Rate",
                table: "Request",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "TrackingId",
                table: "Request",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Request",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AppProfile",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequestKey",
                value: new Guid("b5bb40d5-917f-4e93-badb-f9ce96fcfd9a"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "TrackingId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Request");

            migrationBuilder.RenameColumn(
                name: "UpdateDateUtc",
                table: "Request",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedDateUtc",
                table: "Request",
                newName: "CreatedAt");

            migrationBuilder.UpdateData(
                table: "AppProfile",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequestKey",
                value: new Guid("38a89605-4021-4905-83ed-19ac7732a0ab"));
        }
    }
}
