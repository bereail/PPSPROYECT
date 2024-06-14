using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniMarket_Server_dev.Migrations
{
    /// <inheritdoc />
    public partial class DomainModelsupdateforOrderDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserType",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "ProductImages",
                newName: "URL");

            migrationBuilder.RenameColumn(
                name: "ImageSize",
                table: "ProductImages",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "ProductImages",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ImageExtension",
                table: "ProductImages",
                newName: "Extension");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(300)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)");

            migrationBuilder.AddColumn<string>(
                name: "Item",
                table: "Details",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Province",
                table: "DeliveryAddresses",
                type: "nvarchar(55)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Item",
                table: "Details");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "UserType");

            migrationBuilder.RenameColumn(
                name: "URL",
                table: "ProductImages",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "ProductImages",
                newName: "ImageSize");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ProductImages",
                newName: "ImageName");

            migrationBuilder.RenameColumn(
                name: "Extension",
                table: "ProductImages",
                newName: "ImageExtension");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(150)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)");

            migrationBuilder.AlterColumn<string>(
                name: "Province",
                table: "DeliveryAddresses",
                type: "nvarchar(45)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(55)");
        }
    }
}
