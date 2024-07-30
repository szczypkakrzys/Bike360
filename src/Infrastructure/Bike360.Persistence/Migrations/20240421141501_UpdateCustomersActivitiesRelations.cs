using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bike360.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomersActivitiesRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntireAmountPaymentAmountPaid",
                table: "CustomersToursRelations",
                newName: "EntireCostLeftToPay");

            migrationBuilder.RenameColumn(
                name: "AdvancedPaymentAmountPaid",
                table: "CustomersToursRelations",
                newName: "AdvancedPaymentLeftToPay");

            migrationBuilder.RenameColumn(
                name: "EntireAmountPaymentAmountPaid",
                table: "CustomersDivingCoursesRelations",
                newName: "EntireCostLeftToPay");

            migrationBuilder.RenameColumn(
                name: "AdvancedPaymentAmountPaid",
                table: "CustomersDivingCoursesRelations",
                newName: "AdvancedPaymentLeftToPay");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntireCostLeftToPay",
                table: "CustomersToursRelations",
                newName: "EntireAmountPaymentAmountPaid");

            migrationBuilder.RenameColumn(
                name: "AdvancedPaymentLeftToPay",
                table: "CustomersToursRelations",
                newName: "AdvancedPaymentAmountPaid");

            migrationBuilder.RenameColumn(
                name: "EntireCostLeftToPay",
                table: "CustomersDivingCoursesRelations",
                newName: "EntireAmountPaymentAmountPaid");

            migrationBuilder.RenameColumn(
                name: "AdvancedPaymentLeftToPay",
                table: "CustomersDivingCoursesRelations",
                newName: "AdvancedPaymentAmountPaid");
        }
    }
}
