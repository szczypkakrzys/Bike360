using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bike360.Identity.Migrations
{
    /// <inheritdoc />
    public partial class addUserNotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConnectedCustomersIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimeCreatedInUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeLastModifiedInUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                values: new object[] { "80b74a77-ba7d-4d1a-86b8-4aa71e9527cf", "AQAAAAIAAYagAAAAEGeEgfwvk0jJ0jy0+Z9CJTm4Rw2b9Dj6u74EuB+wg1hIv8mmAFWpYWoJVhSizzeF3g==", "1874a4a0-6ab9-4812-8d73-d5722825922d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fd7be93d-a089-4801-9de5-c5aa64c97962",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7cf4d48e-3082-4a6c-ae27-ad421ec2b4f9", "AQAAAAIAAYagAAAAEJxW/pq7GekyQ8jjhFKesCDBX4h+wX/tKAC6K4HdcumU+yBaHH/8EPvi/5aAaeJI1g==", "4247789f-5a71-4486-99fc-b22e127b6294" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ApplicationUserId",
                table: "Notifications",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

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
