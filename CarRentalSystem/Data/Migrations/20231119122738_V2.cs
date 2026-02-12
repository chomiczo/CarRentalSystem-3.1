using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalSystem.Data.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CustomerModel",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "CarModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId1",
                table: "CarModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerModel_UserId",
                table: "CustomerModel",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CarModel_CustomerId1",
                table: "CarModel",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CarModel_CustomerModel_CustomerId1",
                table: "CarModel",
                column: "CustomerId1",
                principalTable: "CustomerModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerModel_AspNetUsers_UserId",
                table: "CustomerModel",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarModel_CustomerModel_CustomerId1",
                table: "CarModel");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerModel_AspNetUsers_UserId",
                table: "CustomerModel");

            migrationBuilder.DropIndex(
                name: "IX_CustomerModel_UserId",
                table: "CustomerModel");

            migrationBuilder.DropIndex(
                name: "IX_CarModel_CustomerId1",
                table: "CarModel");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CustomerModel");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "CarModel");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "CarModel");
        }
    }
}
