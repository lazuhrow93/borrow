using System;
using Borrow.Models.Backend;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrow.Migrations
{
    /// <inheritdoc />
    public partial class NewRequestTableInfoAndItem3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>( //manual added
                name: "Type",
                table: "Request",
                type: "int",
                nullable: false,
                defaultValue: Request.RequestType.Daily);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Request",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Request",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Request"
            );
        }
    }
}
