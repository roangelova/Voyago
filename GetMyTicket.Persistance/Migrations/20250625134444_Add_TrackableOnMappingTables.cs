using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetMyTicket.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Add_TrackableOnMappingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UserPassengerMap",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "UserPassengerMap",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "UserPassengerMap",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserPassengerMap",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "UserPassengerMap",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PassengerBookingMap",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "PassengerBookingMap",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PassengerBookingMap",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PassengerBookingMap",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "PassengerBookingMap",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UserPassengerMap");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "UserPassengerMap");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserPassengerMap");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserPassengerMap");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "UserPassengerMap");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PassengerBookingMap");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "PassengerBookingMap");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PassengerBookingMap");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PassengerBookingMap");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "PassengerBookingMap");
        }
    }
}
