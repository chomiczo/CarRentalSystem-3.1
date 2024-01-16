using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalSystem.Data.Migrations
{
    public partial class V8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarModel_CustomerModel_CustomerId1",
                table: "CarModel");

            migrationBuilder.DropIndex(
                name: "IX_CarModel_CustomerId1",
                table: "CarModel");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "CarModel");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "CarModel",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarModel_CustomerId",
                table: "CarModel",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarModel_CustomerModel_CustomerId",
                table: "CarModel",
                column: "CustomerId",
                principalTable: "CustomerModel",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarModel_CustomerModel_CustomerId",
                table: "CarModel");

            migrationBuilder.DropIndex(
                name: "IX_CarModel_CustomerId",
                table: "CarModel");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "CarModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId1",
                table: "CarModel",
                type: "int",
                nullable: true);

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
        }
    }
}
