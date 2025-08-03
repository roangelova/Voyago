using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetMyTicket.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Update_Discount_renameMinimumAmountProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinimalAmount",
                table: "Discounts",
                newName: "MinimumAmount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinimumAmount",
                table: "Discounts",
                newName: "MinimalAmount");
        }
    }
}
