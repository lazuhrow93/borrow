using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrow.Migrations
{
    /// <inheritdoc />
    public partial class AddingMigratedDAta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppProfile",
                columns: new[] { "Id", "NeighborhoodId", "OwnerId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Neighborhood",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Spring Lakes" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppProfile",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Neighborhood",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
