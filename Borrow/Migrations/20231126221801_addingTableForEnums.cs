using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrow.Migrations
{
    /// <inheritdoc />
    public partial class addingTableForEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BorrowEnumerations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrackingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowEnumerations", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AppProfile",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequestKey",
                value: new Guid("ba81dcdd-b56a-4aae-a713-dd31be224917"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowEnumerations");

            migrationBuilder.UpdateData(
                table: "AppProfile",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequestKey",
                value: new Guid("9b44c98c-c43d-46de-8090-ad4a7f675718"));
        }
    }
}
