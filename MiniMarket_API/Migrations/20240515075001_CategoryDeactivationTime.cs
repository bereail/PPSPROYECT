using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniMarket_Server_dev.Migrations
{
    /// <inheritdoc />
    public partial class CategoryDeactivationTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivationTime",
                table: "Categories",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeactivationTime",
                table: "Categories");
        }
    }
}
