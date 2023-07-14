using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrow.Migrations
{
    /// <inheritdoc />
    public partial class CounterOfferColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            //migrationBuilder.UpdateData(
            //    table: "AppProfile",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    column: "RequestKey",
            //    value: new Guid("013c8e96-38de-4057-9819-60902b04d667"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CounterRate",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "CounterType",
                table: "Request");

            //migrationBuilder.UpdateData(
            //    table: "AppProfile",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    column: "RequestKey",
            //    value: new Guid("b5bb40d5-917f-4e93-badb-f9ce96fcfd9a"));
        }
    }
}
