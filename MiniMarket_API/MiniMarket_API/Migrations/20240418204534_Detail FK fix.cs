using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniMarket_Server_dev.Migrations
{
    /// <inheritdoc />
    public partial class DetailFKfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Details_Orders_SaleOrderId",
                table: "Details");

            migrationBuilder.DropIndex(
                name: "IX_Details_SaleOrderId",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "SaleOrderId",
                table: "Details");

            migrationBuilder.CreateIndex(
                name: "IX_Details_OrderId",
                table: "Details",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Details_Orders_OrderId",
                table: "Details",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Details_Orders_OrderId",
                table: "Details");

            migrationBuilder.DropIndex(
                name: "IX_Details_OrderId",
                table: "Details");

            migrationBuilder.AddColumn<Guid>(
                name: "SaleOrderId",
                table: "Details",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Details_SaleOrderId",
                table: "Details",
                column: "SaleOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Details_Orders_SaleOrderId",
                table: "Details",
                column: "SaleOrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
