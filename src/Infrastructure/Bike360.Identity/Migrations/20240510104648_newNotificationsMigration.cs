using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bike360.Identity.Migrations
{
    /// <inheritdoc />
    public partial class newNotificationsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectedCustomersIds",
                table: "Notifications");

            migrationBuilder.AddColumn<int>(
                name: "ActivityId",
                table: "Notifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomersIdsList",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90ebb8bd-27fc-4c78-968f-9d93d267a502",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14a769f0-d47b-47aa-a9fa-047f000a1866", "AQAAAAIAAYagAAAAEEFJ6FqxC06mB6xp+TNs5EFmfrOOhiU7pUyJYN2/9JVZ41z3Qchzl2tTbA131Xf4oA==", "16cd4e1e-a798-4ff3-b498-3b866b6f735c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fd7be93d-a089-4801-9de5-c5aa64c97962",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1fc77b39-b311-404b-af3e-9b067083d2ac", "AQAAAAIAAYagAAAAEATMznM+0dJYDUIXrWpOCywjjY5CJXsgJLh+lbIBy10zHzbAVtFFVq8zOFkdJEcjtA==", "282a6e73-829e-441a-af2b-44dc820f9895" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CustomersIdsList",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "ConnectedCustomersIds",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);

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
    }
}
