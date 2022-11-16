using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS.DAL.Migrations
{
    public partial class vehicletriprelationcustom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleNumberPlate",
                table: "Trips");

            migrationBuilder.AddColumn<Guid>(
                name: "Vehicle",
                table: "Trips",
                type: "uniqueidentifier",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_Vehicle",
                table: "Trips",
                column: "Vehicle");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Vehicles_Vehicle",
                table: "Trips",
                column: "Vehicle",
                principalTable: "Vehicles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Vehicles_Vehicle",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_Vehicle",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Vehicle",
                table: "Trips");

            migrationBuilder.AddColumn<string>(
                name: "VehicleNumberPlate",
                table: "Trips",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: false,
                defaultValue: "");
        }
    }
}
