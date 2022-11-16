using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS.DAL.Migrations
{
    public partial class vehicleownerrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Users_UserId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_UserId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<Guid>(
                name: "Owner",
                table: "Vehicles",
                type: "uniqueidentifier",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Owner",
                table: "Vehicles",
                column: "Owner");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Users_Owner",
                table: "Vehicles",
                column: "Owner",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Users_Owner",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_Owner",
                table: "Vehicles");

            migrationBuilder.AlterColumn<string>(
                name: "Owner",
                table: "Vehicles",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Vehicles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_UserId",
                table: "Vehicles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Users_UserId",
                table: "Vehicles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
