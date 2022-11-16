using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS.DAL.Migrations
{
    public partial class noofpassengers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfPasengers",
                table: "Vehicles",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfPasengers",
                table: "Vehicles");
        }
    }
}
