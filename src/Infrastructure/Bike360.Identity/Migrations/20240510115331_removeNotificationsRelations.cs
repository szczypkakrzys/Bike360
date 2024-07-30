using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bike360.Identity.Migrations
{
    /// <inheritdoc />
    public partial class removeNotificationsRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90ebb8bd-27fc-4c78-968f-9d93d267a502",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b85ed4ba-de55-4db4-a546-cec1da3f99f6", "AQAAAAIAAYagAAAAENo8SoKOSyndxKf996QIzvoWwiYOSRb2BU10lhjaAdIocN39DI4p+ebMfwFYLfBzRA==", "a3344fbf-cf62-4708-9310-b7594dab7bdf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fd7be93d-a089-4801-9de5-c5aa64c97962",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8b089f60-48cd-4cb5-ad6b-78ac7e6ad1ca", "AQAAAAIAAYagAAAAEE3wESN6w38+l61monzgcvWcm9qsAqxXf1u7YvZo77WcJ+IZuUqXx25nYH+0Wbul2Q==", "66a15794-1d7b-48f5-b12f-d35054d02651" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomersIdsList = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeCreatedInUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeLastModifiedInUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ApplicationUserId",
                table: "Notifications",
                column: "ApplicationUserId");
        }
    }
}
