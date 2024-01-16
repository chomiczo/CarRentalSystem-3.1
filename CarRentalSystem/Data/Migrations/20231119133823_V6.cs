using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalSystem.Data.Migrations
{
    public partial class V6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "RentalModel",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_RentalModel_CustomerId",
                table: "RentalModel",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalModel_CustomerModel_CustomerId",
                table: "RentalModel",
                column: "CustomerId",
                principalTable: "CustomerModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalModel_CustomerModel_CustomerId",
                table: "RentalModel");

            migrationBuilder.DropIndex(
                name: "IX_RentalModel_CustomerId",
                table: "RentalModel");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "RentalModel",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
