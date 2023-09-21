using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrow.Migrations
{
    /// <inheritdoc />
    public partial class RefactorRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CounterRate",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "CounterType",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Request");

            migrationBuilder.AddColumn<int>(
                name: "ListingId",
                table: "Request",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppProfile",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequestKey",
                value: new Guid("117209dd-9c87-4bb9-8e2b-c568cc8c36ad"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListingId",
                table: "Request");

            migrationBuilder.AddColumn<decimal>(
                name: "CounterRate",
                table: "Request",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "CounterType",
                table: "Request",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Rate",
                table: "Request",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AppProfile",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequestKey",
                value: new Guid("ea4f73b2-f731-48ae-9903-17b0b73e3e92"));
        }
    }
}
