using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalSystem.Data.Migrations
{
    public partial class V11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarModelId",
                table: "RentalModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalModel_CarModelId",
                table: "RentalModel",
                column: "CarModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalModel_CarModel_CarModelId",
                table: "RentalModel",
                column: "CarModelId",
                principalTable: "CarModel",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalModel_CarModel_CarModelId",
                table: "RentalModel");

            migrationBuilder.DropIndex(
                name: "IX_RentalModel_CarModelId",
                table: "RentalModel");

            migrationBuilder.DropColumn(
                name: "CarModelId",
                table: "RentalModel");
        }
    }
}
