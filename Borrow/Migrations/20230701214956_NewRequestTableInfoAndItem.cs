using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrow.Migrations
{
    /// <inheritdoc />
    public partial class NewRequestTableInfoAndItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BorrowRequests",
                table: "BorrowRequests");

            migrationBuilder.RenameTable(
                name: "BorrowRequests",
                newName: "Request");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Request",
                table: "Request",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropPrimaryKey(
                name: "PK_Request",
                table: "Request");

            migrationBuilder.RenameTable(
                name: "Request",
                newName: "BorrowRequests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BorrowRequests",
                table: "BorrowRequests",
                column: "Id");
        }
    }
}
