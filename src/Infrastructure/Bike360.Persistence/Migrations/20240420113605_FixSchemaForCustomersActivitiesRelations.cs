using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bike360.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixSchemaForCustomersActivitiesRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdvancePaymentDeadline",
                table: "CustomersToursRelations");

            migrationBuilder.DropColumn(
                name: "EntireAmountPaymentDeadline",
                table: "CustomersToursRelations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CustomersToursRelations");

            migrationBuilder.DropColumn(
                name: "TimeEnd",
                table: "CustomersToursRelations");

            migrationBuilder.DropColumn(
                name: "AdvancePaymentDeadline",
                table: "CustomersDivingCoursesRelations");

            migrationBuilder.DropColumn(
                name: "EntireAmountPaymentDeadline",
                table: "CustomersDivingCoursesRelations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CustomersDivingCoursesRelations");

            migrationBuilder.DropColumn(
                name: "TimeEnd",
                table: "CustomersDivingCoursesRelations");

            migrationBuilder.RenameColumn(
                name: "TimeStart",
                table: "CustomersToursRelations",
                newName: "EnrollmentDate");

            migrationBuilder.RenameColumn(
                name: "EntireCost",
                table: "CustomersToursRelations",
                newName: "EntireAmountPaymentAmountPaid");

            migrationBuilder.RenameColumn(
                name: "AdvancePaymentCost",
                table: "CustomersToursRelations",
                newName: "AdvancedPaymentAmountPaid");

            migrationBuilder.RenameColumn(
                name: "TimeStart",
                table: "CustomersDivingCoursesRelations",
                newName: "EnrollmentDate");

            migrationBuilder.RenameColumn(
                name: "EntireCost",
                table: "CustomersDivingCoursesRelations",
                newName: "EntireAmountPaymentAmountPaid");

            migrationBuilder.RenameColumn(
                name: "AdvancePaymentCost",
                table: "CustomersDivingCoursesRelations",
                newName: "AdvancedPaymentAmountPaid");

            migrationBuilder.AddColumn<DateOnly>(
                name: "AdvancedPaymentDate",
                table: "CustomersToursRelations",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EntireAmountPaymentDate",
                table: "CustomersToursRelations",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "AdvancedPaymentDate",
                table: "CustomersDivingCoursesRelations",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EntireAmountPaymentDate",
                table: "CustomersDivingCoursesRelations",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdvancedPaymentDate",
                table: "CustomersToursRelations");

            migrationBuilder.DropColumn(
                name: "EntireAmountPaymentDate",
                table: "CustomersToursRelations");

            migrationBuilder.DropColumn(
                name: "AdvancedPaymentDate",
                table: "CustomersDivingCoursesRelations");

            migrationBuilder.DropColumn(
                name: "EntireAmountPaymentDate",
                table: "CustomersDivingCoursesRelations");

            migrationBuilder.RenameColumn(
                name: "EntireAmountPaymentAmountPaid",
                table: "CustomersToursRelations",
                newName: "EntireCost");

            migrationBuilder.RenameColumn(
                name: "EnrollmentDate",
                table: "CustomersToursRelations",
                newName: "TimeStart");

            migrationBuilder.RenameColumn(
                name: "AdvancedPaymentAmountPaid",
                table: "CustomersToursRelations",
                newName: "AdvancePaymentCost");

            migrationBuilder.RenameColumn(
                name: "EntireAmountPaymentAmountPaid",
                table: "CustomersDivingCoursesRelations",
                newName: "EntireCost");

            migrationBuilder.RenameColumn(
                name: "EnrollmentDate",
                table: "CustomersDivingCoursesRelations",
                newName: "TimeStart");

            migrationBuilder.RenameColumn(
                name: "AdvancedPaymentAmountPaid",
                table: "CustomersDivingCoursesRelations",
                newName: "AdvancePaymentCost");

            migrationBuilder.AddColumn<DateOnly>(
                name: "AdvancePaymentDeadline",
                table: "CustomersToursRelations",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "EntireAmountPaymentDeadline",
                table: "CustomersToursRelations",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CustomersToursRelations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "TimeEnd",
                table: "CustomersToursRelations",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "AdvancePaymentDeadline",
                table: "CustomersDivingCoursesRelations",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "EntireAmountPaymentDeadline",
                table: "CustomersDivingCoursesRelations",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CustomersDivingCoursesRelations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "TimeEnd",
                table: "CustomersDivingCoursesRelations",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}
