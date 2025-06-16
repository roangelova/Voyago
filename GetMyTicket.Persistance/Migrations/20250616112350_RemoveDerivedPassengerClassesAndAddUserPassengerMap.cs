using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetMyTicket.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDerivedPassengerClassesAndAddUserPassengerMap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Passenger_PassengerMapId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BaggageItem_Passenger_PassengerId",
                table: "BaggageItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PassengerBookingMap_Passenger_PassengerId",
                table: "PassengerBookingMap");

            migrationBuilder.DropTable(
                name: "Passenger");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PassengerMapId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PassengerMapId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Logo",
                table: "TransportationProviders",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    PassengerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PassengerType = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DocumentType = table.Column<int>(type: "int", nullable: true),
                    DocumentId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ExpirationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DOB = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.PassengerId);
                });

            migrationBuilder.CreateTable(
                name: "UserPassengerMap",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PassengerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPassengerMap", x => new { x.UserId, x.PassengerId });
                    table.ForeignKey(
                        name: "FK_UserPassengerMap_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPassengerMap_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "PassengerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPassengerMap_PassengerId",
                table: "UserPassengerMap",
                column: "PassengerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaggageItem_Passengers_PassengerId",
                table: "BaggageItem",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "PassengerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PassengerBookingMap_Passengers_PassengerId",
                table: "PassengerBookingMap",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "PassengerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaggageItem_Passengers_PassengerId",
                table: "BaggageItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PassengerBookingMap_Passengers_PassengerId",
                table: "PassengerBookingMap");

            migrationBuilder.DropTable(
                name: "UserPassengerMap");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Logo",
                table: "TransportationProviders",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PassengerMapId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Passenger",
                columns: table => new
                {
                    PassengerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DOB = table.Column<DateOnly>(type: "date", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DocumentId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DocumentType = table.Column<int>(type: "int", nullable: true),
                    ExpirationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    SeatNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Child_SeatNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravellingWithAStroller = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passenger", x => x.PassengerId);
                    table.ForeignKey(
                        name: "FK_Passenger_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PassengerMapId",
                table: "AspNetUsers",
                column: "PassengerMapId",
                unique: true,
                filter: "[PassengerMapId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Passenger_UserId",
                table: "Passenger",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Passenger_PassengerMapId",
                table: "AspNetUsers",
                column: "PassengerMapId",
                principalTable: "Passenger",
                principalColumn: "PassengerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaggageItem_Passenger_PassengerId",
                table: "BaggageItem",
                column: "PassengerId",
                principalTable: "Passenger",
                principalColumn: "PassengerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PassengerBookingMap_Passenger_PassengerId",
                table: "PassengerBookingMap",
                column: "PassengerId",
                principalTable: "Passenger",
                principalColumn: "PassengerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
