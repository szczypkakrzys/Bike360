using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bike360.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PaymentIntToDoubleType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "CustomersToursRelations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CustomersDivingCoursesRelations");

            migrationBuilder.AlterColumn<double>(
                name: "EntireCost",
                table: "Tours",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "AdvancePaymentCost",
                table: "Tours",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "EntireCost",
                table: "DivingCourses",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "AdvancePaymentCost",
                table: "DivingCourses",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "EntireCostLeftToPay",
                table: "CustomersToursRelations",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "AdvancedPaymentLeftToPay",
                table: "CustomersToursRelations",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "EntireCostLeftToPay",
                table: "CustomersDivingCoursesRelations",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "AdvancedPaymentLeftToPay",
                table: "CustomersDivingCoursesRelations",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EntireCost",
                table: "Tours",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "AdvancePaymentCost",
                table: "Tours",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "EntireCost",
                table: "DivingCourses",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "AdvancePaymentCost",
                table: "DivingCourses",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "EntireCostLeftToPay",
                table: "CustomersToursRelations",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "AdvancedPaymentLeftToPay",
                table: "CustomersToursRelations",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "CustomersToursRelations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "EntireCostLeftToPay",
                table: "CustomersDivingCoursesRelations",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "AdvancedPaymentLeftToPay",
                table: "CustomersDivingCoursesRelations",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "CustomersDivingCoursesRelations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
