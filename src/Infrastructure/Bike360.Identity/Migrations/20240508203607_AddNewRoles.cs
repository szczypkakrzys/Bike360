using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bike360.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddNewRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5bb93284-b026-4ed1-a331-5533e757a143",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Diving School Employee", "DIVING SCHOOL EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d763c0cd-a5bc-4721-8421-ac4a37071495", null, "Travel Agency Employee", "TRAVEL AGENCY ADMINISTRATOR" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d763c0cd-a5bc-4721-8421-ac4a37071495");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5bb93284-b026-4ed1-a331-5533e757a143",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Employee", "EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90ebb8bd-27fc-4c78-968f-9d93d267a502",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4579bc2c-2b6f-4859-b8aa-947790f332e4", "AQAAAAIAAYagAAAAEIFXlkPvXZuPo1p1pdVyUUhjcvOJ/UhePoggghYopRMNLbgqiIMzi1a36RG8syrqbw==", "4f3d0f1e-251f-4baa-9bd4-c4c4be25c8fb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fd7be93d-a089-4801-9de5-c5aa64c97962",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "516498bf-7da1-4488-92b9-b79e78556830", "AQAAAAIAAYagAAAAEBCqd6HGJL9ZkDqpL1Whk0PvxarUn2D2pBSxXa7pv3geHgrE/ufOsu9WtctjJAUldA==", "1ff6d419-4c5b-44bb-931e-6610c6b0d62b" });
        }
    }
}
