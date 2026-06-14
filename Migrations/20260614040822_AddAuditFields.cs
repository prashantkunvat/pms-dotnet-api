using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "ProjectTasks",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "ProjectTasks",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "ProjectTasks",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Projects",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "Projects",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Projects",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "ProductCategories",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "ProductCategories",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ProductCategories",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "ProductCategories",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProjectTasks");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ProjectTasks");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ProjectTasks");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ProductCategories");
        }
    }
}
