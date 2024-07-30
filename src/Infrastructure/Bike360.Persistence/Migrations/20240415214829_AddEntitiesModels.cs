using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bike360.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEntitiesModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Voivodeship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeCreatedInUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeLastModifiedInUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DivingCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreatedInUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeLastModifiedInUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStart = table.Column<DateOnly>(type: "date", nullable: false),
                    TimeEnd = table.Column<DateOnly>(type: "date", nullable: false),
                    EntireCost = table.Column<int>(type: "int", nullable: false),
                    AdvancePaymentCost = table.Column<int>(type: "int", nullable: false),
                    EntireAmountPaymentDeadline = table.Column<DateOnly>(type: "date", nullable: false),
                    AdvancePaymentDeadline = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivingCourses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreatedInUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeLastModifiedInUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DivingSchoolCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DivingCertificationLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeCreatedInUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeLastModifiedInUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivingSchoolCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DivingSchoolCustomers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TravelAgencyCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreatedInUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeLastModifiedInUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelAgencyCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelAgencyCustomers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomersDivingCoursesRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    DivingCourseId = table.Column<int>(type: "int", nullable: false),
                    TimeCreatedInUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeLastModifiedInUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStart = table.Column<DateOnly>(type: "date", nullable: false),
                    TimeEnd = table.Column<DateOnly>(type: "date", nullable: false),
                    EntireCost = table.Column<int>(type: "int", nullable: false),
                    AdvancePaymentCost = table.Column<int>(type: "int", nullable: false),
                    EntireAmountPaymentDeadline = table.Column<DateOnly>(type: "date", nullable: false),
                    AdvancePaymentDeadline = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomersDivingCoursesRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomersDivingCoursesRelations_DivingCourses_DivingCourseId",
                        column: x => x.DivingCourseId,
                        principalTable: "DivingCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomersDivingCoursesRelations_DivingSchoolCustomers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "DivingSchoolCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomersToursRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    TimeCreatedInUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeLastModifiedInUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStart = table.Column<DateOnly>(type: "date", nullable: false),
                    TimeEnd = table.Column<DateOnly>(type: "date", nullable: false),
                    EntireCost = table.Column<int>(type: "int", nullable: false),
                    AdvancePaymentCost = table.Column<int>(type: "int", nullable: false),
                    EntireAmountPaymentDeadline = table.Column<DateOnly>(type: "date", nullable: false),
                    AdvancePaymentDeadline = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomersToursRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomersToursRelations_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomersToursRelations_TravelAgencyCustomers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "TravelAgencyCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomersDivingCoursesRelations_CustomerId",
                table: "CustomersDivingCoursesRelations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersDivingCoursesRelations_DivingCourseId",
                table: "CustomersDivingCoursesRelations",
                column: "DivingCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersToursRelations_CustomerId",
                table: "CustomersToursRelations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersToursRelations_TourId",
                table: "CustomersToursRelations",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_DivingSchoolCustomers_AddressId",
                table: "DivingSchoolCustomers",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TravelAgencyCustomers_AddressId",
                table: "TravelAgencyCustomers",
                column: "AddressId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomersDivingCoursesRelations");

            migrationBuilder.DropTable(
                name: "CustomersToursRelations");

            migrationBuilder.DropTable(
                name: "DivingCourses");

            migrationBuilder.DropTable(
                name: "DivingSchoolCustomers");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "TravelAgencyCustomers");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
