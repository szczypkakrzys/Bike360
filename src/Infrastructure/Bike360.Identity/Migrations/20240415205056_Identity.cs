using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bike360.Identity.Migrations
{
    /// <inheritdoc />
    public partial class Identity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90ebb8bd-27fc-4c78-968f-9d93d267a502",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7cc167e6-4dab-469e-b547-ecb51f699513", "AQAAAAIAAYagAAAAEEmm6lwt7TuMAcuET8C6oYTfbc5QJ4PraeSuTT/S/xRqLFTKmJBmlQqHcXdWOJOTwA==", "fc99f990-c85b-480e-a5be-d5f467b7c081" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fd7be93d-a089-4801-9de5-c5aa64c97962",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a7b6710c-46ee-4268-8778-37e7091602b7", "AQAAAAIAAYagAAAAEKGm5QyVzV/Spm24ZwokfH4F2s/66SfnBMm335pBIl0Vaw6Cxi+j7jA54rGapsZJpw==", "c124d89b-9e19-4185-98f0-2e03d0a4163f" });
        }
    }
}
