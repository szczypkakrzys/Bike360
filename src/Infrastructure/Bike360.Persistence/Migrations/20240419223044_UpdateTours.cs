using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bike360.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdvancePaymentCost",
                table: "Tours",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "AdvancePaymentDeadline",
                table: "Tours",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "EntireAmountPaymentDeadline",
                table: "Tours",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "EntireCost",
                table: "Tours",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "TimeEnd",
                table: "Tours",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "TimeStart",
                table: "Tours",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdvancePaymentCost",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "AdvancePaymentDeadline",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "EntireAmountPaymentDeadline",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "EntireCost",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "TimeEnd",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "TimeStart",
                table: "Tours");
        }
    }
}
