using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bike360.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Bike360 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bikes_Customers_CustomerId",
                table: "Bikes");

            migrationBuilder.DropIndex(
                name: "IX_Bikes_CustomerId",
                table: "Bikes");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Bikes");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Bikes",
                newName: "Type");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Bikes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Bikes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Bikes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Bikes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Bikes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Bikes");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Bikes");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Bikes");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Bikes",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Bikes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bikes_CustomerId",
                table: "Bikes",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bikes_Customers_CustomerId",
                table: "Bikes",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
