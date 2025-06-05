using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace apbd12c_cw12.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    IdClient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.IdClient);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    IdCountry = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.IdCountry);
                });

            migrationBuilder.CreateTable(
                name: "Trip",
                columns: table => new
                {
                    IdTrip = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(220)", maxLength: 220, nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxPeople = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip", x => x.IdTrip);
                });

            migrationBuilder.CreateTable(
                name: "Client_Trip",
                columns: table => new
                {
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    IdTrip = table.Column<int>(type: "int", nullable: false),
                    RegisteredAt = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client_Trip", x => new { x.IdClient, x.IdTrip });
                    table.ForeignKey(
                        name: "FK_Client_Trip_Client_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Client",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Client_Trip_Trip_IdTrip",
                        column: x => x.IdTrip,
                        principalTable: "Trip",
                        principalColumn: "IdTrip",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Country_Trip",
                columns: table => new
                {
                    IdCountry = table.Column<int>(type: "int", nullable: false),
                    IdTrip = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country_Trip", x => new { x.IdCountry, x.IdTrip });
                    table.ForeignKey(
                        name: "FK_Country_Trip_Country_IdCountry",
                        column: x => x.IdCountry,
                        principalTable: "Country",
                        principalColumn: "IdCountry",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Country_Trip_Trip_IdTrip",
                        column: x => x.IdTrip,
                        principalTable: "Trip",
                        principalColumn: "IdTrip",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "IdClient", "Email", "FirstName", "LastName", "Pesel", "Telephone" },
                values: new object[,]
                {
                    { 1, "jan.kowalski@gmail.com", "Jan", "Kowalski", "12345678910", "123456789" },
                    { 2, "anna.nowak@gmail.com", "Anna", "Nowak", "11111222333", "111222333" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "IdCountry", "Name" },
                values: new object[,]
                {
                    { 1, "Polska" },
                    { 2, "Niemcy" },
                    { 3, "Słowacja" }
                });

            migrationBuilder.InsertData(
                table: "Trip",
                columns: new[] { "IdTrip", "DateFrom", "DateTo", "Description", "MaxPeople", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wycieczka kulturoznawcza po Berlinie", 16, "Berlin" },
                    { 2, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wycieczka plażoznawcza po Gdańsku", 20, "Gdańsk" },
                    { 3, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wycieczka zamkoznawcza po Bratysławie", 12, "Bratysława" }
                });

            migrationBuilder.InsertData(
                table: "Client_Trip",
                columns: new[] { "IdClient", "IdTrip", "PaymentDate", "RegisteredAt" },
                values: new object[] { 1, 1, null, 20250601 });

            migrationBuilder.InsertData(
                table: "Country_Trip",
                columns: new[] { "IdCountry", "IdTrip" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 1 },
                    { 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_Pesel",
                table: "Client",
                column: "Pesel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Client_Trip_IdTrip",
                table: "Client_Trip",
                column: "IdTrip");

            migrationBuilder.CreateIndex(
                name: "IX_Country_Trip_IdTrip",
                table: "Country_Trip",
                column: "IdTrip");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Client_Trip");

            migrationBuilder.DropTable(
                name: "Country_Trip");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Trip");
        }
    }
}
