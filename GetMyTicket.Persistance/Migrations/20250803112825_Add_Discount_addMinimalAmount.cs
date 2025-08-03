using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetMyTicket.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Add_Discount_addMinimalAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MinimalAmount",
                table: "Discounts",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinimalAmount",
                table: "Discounts");
        }
    }
}
