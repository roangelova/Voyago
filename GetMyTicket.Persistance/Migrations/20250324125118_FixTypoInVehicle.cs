using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetMyTicket.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class FixTypoInVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_TransportationProviders_TransportationProvideriD",
                table: "Vehicle");

            migrationBuilder.RenameColumn(
                name: "TransportationProvideriD",
                table: "Vehicle",
                newName: "TransportationProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_TransportationProvideriD",
                table: "Vehicle",
                newName: "IX_Vehicle_TransportationProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_TransportationProviders_TransportationProviderId",
                table: "Vehicle",
                column: "TransportationProviderId",
                principalTable: "TransportationProviders",
                principalColumn: "TransportationProviderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_TransportationProviders_TransportationProviderId",
                table: "Vehicle");

            migrationBuilder.RenameColumn(
                name: "TransportationProviderId",
                table: "Vehicle",
                newName: "TransportationProvideriD");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_TransportationProviderId",
                table: "Vehicle",
                newName: "IX_Vehicle_TransportationProvideriD");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_TransportationProviders_TransportationProvideriD",
                table: "Vehicle",
                column: "TransportationProvideriD",
                principalTable: "TransportationProviders",
                principalColumn: "TransportationProviderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
