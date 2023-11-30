using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrow.Migrations
{
    /// <inheritdoc />
    public partial class RemovingEnumsAndMakingIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Request");

            migrationBuilder.RenameColumn(
                name: "SuggestedMeetingTime",
                table: "Request",
                newName: "RequesterSuggestedMeetingTime");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Request",
                newName: "TermId");

            migrationBuilder.AddColumn<DateTime>(
                name: "LenderSuggestedMeetingTime",
                table: "Request",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PendingActionFromId",
                table: "Request",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Request",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.UpdateData(
            //    table: "AppProfile",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    column: "RequestKey",
            //    value: new Guid("9b44c98c-c43d-46de-8090-ad4a7f675718"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LenderSuggestedMeetingTime",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "PendingActionFromId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Request");

            migrationBuilder.RenameColumn(
                name: "TermId",
                table: "Request",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "RequesterSuggestedMeetingTime",
                table: "Request",
                newName: "SuggestedMeetingTime");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Request",
                type: "int",
                nullable: true);

            //migrationBuilder.UpdateData(
            //    table: "AppProfile",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    column: "RequestKey",
            //    value: new Guid("0c7d439b-8ea5-47e6-b110-a5aec17bb301"));
        }
    }
}
