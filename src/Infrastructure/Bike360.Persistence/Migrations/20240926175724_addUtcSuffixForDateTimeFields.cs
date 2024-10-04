using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bike360.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addUtcSuffixForDateTimeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTimeStart",
                table: "Reservations",
                newName: "DateTimeStartInUtc");

            migrationBuilder.RenameColumn(
                name: "DateTimeEnd",
                table: "Reservations",
                newName: "DateTimeEndInUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTimeStartInUtc",
                table: "Reservations",
                newName: "DateTimeStart");

            migrationBuilder.RenameColumn(
                name: "DateTimeEndInUtc",
                table: "Reservations",
                newName: "DateTimeEnd");
        }
    }
}
