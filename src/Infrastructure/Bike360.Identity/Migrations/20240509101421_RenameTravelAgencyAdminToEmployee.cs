using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bike360.Identity.Migrations
{
    /// <inheritdoc />
    public partial class RenameTravelAgencyAdminToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d763c0cd-a5bc-4721-8421-ac4a37071495",
                column: "NormalizedName",
                value: "TRAVEL AGENCY EMPLOYEE");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90ebb8bd-27fc-4c78-968f-9d93d267a502",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "250a4c1e-9fb3-4dde-ad04-adc57606ba84", "AQAAAAIAAYagAAAAEPMKUaUf5Ssze+i4xPOCErtnLbw4NVTGYePnWx0wrwHhxgY3upOahyYBAq5memfykw==", "73fa560f-441e-42d2-ba9c-3d9d0ee91244" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fd7be93d-a089-4801-9de5-c5aa64c97962",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b1a89973-7af7-4396-a3e0-c8fce7e4f477", "AQAAAAIAAYagAAAAEItSQJEPKgg/3Z1zzGrL97NAP0APJfyaRSP2m8hG+b27xtkTgvJ7/kTo2UxbxYQ6cg==", "95134974-3383-43e7-82a5-56c2b95b0f8b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d763c0cd-a5bc-4721-8421-ac4a37071495",
                column: "NormalizedName",
                value: "TRAVEL AGENCY ADMINISTRATOR");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90ebb8bd-27fc-4c78-968f-9d93d267a502",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "25ade228-fbc9-4e89-9812-6cc674e04432", "AQAAAAIAAYagAAAAEImm3yeM8RGUHBbfquqAAt0DfJt0xJCX6sZa55hhfDtJ4rGDNPIk4TWCzap6Eta0tg==", "cbfc4650-8278-4837-8d58-9f89dfa6245b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fd7be93d-a089-4801-9de5-c5aa64c97962",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e8836f20-f607-4744-b478-29aa61fa1020", "AQAAAAIAAYagAAAAEC1VhZH58fDML8n1NB9L935TD2pvZKl7f7dKTxJ8k7SbY+9s4oefs8FBoTHeMglPRQ==", "2a4e3432-0040-4d01-84f9-3ffe178f61f2" });
        }
    }
}
