using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentingSystem.Data.Migrations
{
    public partial class UserCarRentDateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentDays",
                table: "UsersCars");

            migrationBuilder.AddColumn<DateTime>(
                name: "RentDate",
                table: "UsersCars",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentDate",
                table: "UsersCars");

            migrationBuilder.AddColumn<int>(
                name: "RentDays",
                table: "UsersCars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
