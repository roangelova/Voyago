using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetMyTicket.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Add_BaggagePrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VehicleId",
                table: "Vehicle",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TripId",
                table: "Trips",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TransportationProviderId",
                table: "TransportationProviders",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PassengerId",
                table: "Passengers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "Countries",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Cities",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "Bookings",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BaggageItemId",
                table: "BaggageItem",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "BaggagePrice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransportationProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaggageSize = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaggagePrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaggagePrice_TransportationProviders_TransportationProviderId",
                        column: x => x.TransportationProviderId,
                        principalTable: "TransportationProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaggagePrice_TransportationProviderId",
                table: "BaggagePrice",
                column: "TransportationProviderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaggagePrice");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Vehicle",
                newName: "VehicleId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Trips",
                newName: "TripId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TransportationProviders",
                newName: "TransportationProviderId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Passengers",
                newName: "PassengerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Countries",
                newName: "CountryId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Cities",
                newName: "CityId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Bookings",
                newName: "BookingId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BaggageItem",
                newName: "BaggageItemId");
        }
    }
}
