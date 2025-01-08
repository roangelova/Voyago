using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetMyTicket.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddCountyAndCityEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Tips",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<Guid>(
                name: "EndCityId",
                table: "Tips",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StartCityId",
                table: "Tips",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    IATA_Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tips_EndCityId",
                table: "Tips",
                column: "EndCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tips_StartCityId",
                table: "Tips",
                column: "StartCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_Cities_EndCityId",
                table: "Tips",
                column: "EndCityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_Cities_StartCityId",
                table: "Tips",
                column: "StartCityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tips_Cities_EndCityId",
                table: "Tips");

            migrationBuilder.DropForeignKey(
                name: "FK_Tips_Cities_StartCityId",
                table: "Tips");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Tips_EndCityId",
                table: "Tips");

            migrationBuilder.DropIndex(
                name: "IX_Tips_StartCityId",
                table: "Tips");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Tips");

            migrationBuilder.DropColumn(
                name: "EndCityId",
                table: "Tips");

            migrationBuilder.DropColumn(
                name: "StartCityId",
                table: "Tips");
        }
    }
}
