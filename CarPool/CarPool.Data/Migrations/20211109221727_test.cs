using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPool.Data.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_Ratings_RatingsId",
                table: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "ApplicationUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUsers_RatingsId",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "RatingsId",
                table: "ApplicationUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "AddedByUserId",
                table: "Ratings",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RatingId",
                table: "ApplicationUsers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "ApplicationUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_RatingId",
                table: "ApplicationUsers",
                column: "RatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsers_Ratings_RatingId",
                table: "ApplicationUsers",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_Ratings_RatingId",
                table: "ApplicationUsers");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUsers_RatingId",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "AddedByUserId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "RatingId",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "ApplicationUsers");

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "ApplicationUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "RatingsId",
                table: "ApplicationUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationUserRoles",
                columns: table => new
                {
                    ApplicationRoleId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    ApplicationRoleId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApplicationUserId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserRoles", x => new { x.ApplicationRoleId, x.ApplicationUserId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserRoles_ApplicationRoles_ApplicationRoleId1",
                        column: x => x.ApplicationRoleId1,
                        principalTable: "ApplicationRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRoles_ApplicationUsers_ApplicationUserId1",
                        column: x => x.ApplicationUserId1,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_RatingsId",
                table: "ApplicationUsers",
                column: "RatingsId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRoles_ApplicationRoleId1",
                table: "ApplicationUserRoles",
                column: "ApplicationRoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRoles_ApplicationUserId1",
                table: "ApplicationUserRoles",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsers_Ratings_RatingsId",
                table: "ApplicationUsers",
                column: "RatingsId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
