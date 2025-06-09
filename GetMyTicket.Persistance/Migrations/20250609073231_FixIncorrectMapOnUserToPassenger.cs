using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetMyTicket.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class FixIncorrectMapOnUserToPassenger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Passenger_UserId",
                table: "Passenger");

            migrationBuilder.AlterColumn<Guid>(
                name: "PassengerMapId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Passenger_UserId",
                table: "Passenger",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PassengerMapId",
                table: "AspNetUsers",
                column: "PassengerMapId",
                unique: true,
                filter: "[PassengerMapId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Passenger_PassengerMapId",
                table: "AspNetUsers",
                column: "PassengerMapId",
                principalTable: "Passenger",
                principalColumn: "PassengerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Passenger_PassengerMapId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Passenger_UserId",
                table: "Passenger");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PassengerMapId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "PassengerMapId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passenger_UserId",
                table: "Passenger",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }
    }
}
