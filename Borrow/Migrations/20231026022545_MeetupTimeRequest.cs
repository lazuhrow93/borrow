using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrow.Migrations
{
    /// <inheritdoc />
    public partial class MeetupTimeRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "MeetupTime",
                table: "Request",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SuggestedMeetingTime",
                table: "Request",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppProfile",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequestKey",
                value: new Guid("0c7d439b-8ea5-47e6-b110-a5aec17bb301"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeetupTime",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "SuggestedMeetingTime",
                table: "Request");

            migrationBuilder.UpdateData(
                table: "AppProfile",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequestKey",
                value: new Guid("0895b7e8-b6a8-49dd-afc3-60e009313437"));
        }
    }
}
