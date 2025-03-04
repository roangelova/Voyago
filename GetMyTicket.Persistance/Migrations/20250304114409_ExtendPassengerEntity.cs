using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetMyTicket.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ExtendPassengerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Child_SeatNumber",
                table: "Passenger",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DOB",
                table: "Passenger",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "DocumentId",
                table: "Passenger",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentType",
                table: "Passenger",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ExpirationDate",
                table: "Passenger",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Passenger",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SeatNumber",
                table: "Passenger",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TravellingWithAStroller",
                table: "Passenger",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Child_SeatNumber",
                table: "Passenger");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "Passenger");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Passenger");

            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "Passenger");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Passenger");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Passenger");

            migrationBuilder.DropColumn(
                name: "SeatNumber",
                table: "Passenger");

            migrationBuilder.DropColumn(
                name: "TravellingWithAStroller",
                table: "Passenger");
        }
    }
}
