using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _.Migrations
{
    /// <inheritdoc />
    public partial class FixCategoryColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sortOrder",
                table: "ProductCategories",
                newName: "SortOrder");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "ProductCategories",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SortOrder",
                table: "ProductCategories",
                newName: "sortOrder");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ProductCategories",
                newName: "createdAt");
        }
    }
}
