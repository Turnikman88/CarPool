using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPool.Data.Migrations
{
    public partial class tvae : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Trips_DestinationAddressId",
                table: "Trips",
                column: "DestinationAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_StartAddressId",
                table: "Trips",
                column: "StartAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_DestinationAddresses",
                table: "Trips",
                column: "DestinationAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_StartAddresses",
                table: "Trips",
                column: "StartAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_DestinationAddresses",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_StartAddresses",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_DestinationAddressId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_StartAddressId",
                table: "Trips");
        }
    }
}
