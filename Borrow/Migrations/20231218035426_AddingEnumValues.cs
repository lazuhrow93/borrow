using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrow.Migrations
{
    /// <inheritdoc />
    public partial class AddingEnumValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "BorrowEnumerations",
                newName: "BorrowEnumeration");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BorrowEnumeration",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: null);

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "BorrowEnumeration",
                type: "int",
                nullable: true,
                defaultValue: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "BorrowEnumeration",
                newName: "BorrowEnumerations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BorrowEnumerations");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "BorrowEnumerations");
        }
    }
}
