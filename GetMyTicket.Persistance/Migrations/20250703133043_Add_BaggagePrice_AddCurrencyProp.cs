﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetMyTicket.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Add_BaggagePrice_AddCurrencyProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "BaggagePrice",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "BaggagePrice");
        }
    }
}
