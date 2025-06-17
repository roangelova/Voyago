using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetMyTicket.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class CreateAdultAndChildrenPriceOnTrip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Trips",
                newName: "ChildrenPrice");

            migrationBuilder.AddColumn<double>(
                name: "AdultPrice",
                table: "Trips",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdultPrice",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "ChildrenPrice",
                table: "Trips",
                newName: "Price");
        }
    }
}
