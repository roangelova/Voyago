using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetMyTicket.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AmendTripsTableNameAndFixCapacityIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaggageItem_Tips_TripId",
                table: "BaggageItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Tips_TripId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Tips_Cities_EndCityId",
                table: "Tips");

            migrationBuilder.DropForeignKey(
                name: "FK_Tips_Cities_StartCityId",
                table: "Tips");

            migrationBuilder.DropForeignKey(
                name: "FK_Tips_TransportationProviders_TransportationProviderId",
                table: "Tips");

            migrationBuilder.DropForeignKey(
                name: "FK_Tips_Vehicle_VehicleId",
                table: "Tips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tips",
                table: "Tips");

            migrationBuilder.RenameTable(
                name: "Tips",
                newName: "Trips");

            migrationBuilder.RenameIndex(
                name: "IX_Tips_VehicleId",
                table: "Trips",
                newName: "IX_Trips_VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_Tips_TransportationProviderId",
                table: "Trips",
                newName: "IX_Trips_TransportationProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_Tips_StartCityId",
                table: "Trips",
                newName: "IX_Trips_StartCityId");

            migrationBuilder.RenameIndex(
                name: "IX_Tips_EndCityId",
                table: "Trips",
                newName: "IX_Trips_EndCityId");

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trips",
                table: "Trips",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaggageItem_Trips_TripId",
                table: "BaggageItem",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Trips_TripId",
                table: "Bookings",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Cities_EndCityId",
                table: "Trips",
                column: "EndCityId",
                principalTable: "Cities",
                principalColumn: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Cities_StartCityId",
                table: "Trips",
                column: "StartCityId",
                principalTable: "Cities",
                principalColumn: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_TransportationProviders_TransportationProviderId",
                table: "Trips",
                column: "TransportationProviderId",
                principalTable: "TransportationProviders",
                principalColumn: "TransportationProviderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Vehicle_VehicleId",
                table: "Trips",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaggageItem_Trips_TripId",
                table: "BaggageItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Trips_TripId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Cities_EndCityId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Cities_StartCityId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_TransportationProviders_TransportationProviderId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Vehicle_VehicleId",
                table: "Trips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trips",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Trips");

            migrationBuilder.RenameTable(
                name: "Trips",
                newName: "Tips");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_VehicleId",
                table: "Tips",
                newName: "IX_Tips_VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_TransportationProviderId",
                table: "Tips",
                newName: "IX_Tips_TransportationProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_StartCityId",
                table: "Tips",
                newName: "IX_Tips_StartCityId");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_EndCityId",
                table: "Tips",
                newName: "IX_Tips_EndCityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tips",
                table: "Tips",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaggageItem_Tips_TripId",
                table: "BaggageItem",
                column: "TripId",
                principalTable: "Tips",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Tips_TripId",
                table: "Bookings",
                column: "TripId",
                principalTable: "Tips",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_Cities_EndCityId",
                table: "Tips",
                column: "EndCityId",
                principalTable: "Cities",
                principalColumn: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_Cities_StartCityId",
                table: "Tips",
                column: "StartCityId",
                principalTable: "Cities",
                principalColumn: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_TransportationProviders_TransportationProviderId",
                table: "Tips",
                column: "TransportationProviderId",
                principalTable: "TransportationProviders",
                principalColumn: "TransportationProviderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_Vehicle_VehicleId",
                table: "Tips",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "VehicleId");
        }
    }
}
