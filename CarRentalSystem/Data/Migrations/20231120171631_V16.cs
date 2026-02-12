using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalSystem.Data.Migrations
{
    public partial class V16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RentalModelId",
                table: "CarModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarModel_RentalModelId",
                table: "CarModel",
                column: "RentalModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarModel_RentalModel_RentalModelId",
                table: "CarModel",
                column: "RentalModelId",
                principalTable: "RentalModel",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarModel_RentalModel_RentalModelId",
                table: "CarModel");

            migrationBuilder.DropIndex(
                name: "IX_CarModel_RentalModelId",
                table: "CarModel");

            migrationBuilder.DropColumn(
                name: "RentalModelId",
                table: "CarModel");
        }
    }
}
