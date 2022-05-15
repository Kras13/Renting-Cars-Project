using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentingSystem.Data.Migrations
{
    public partial class RemoveCompositeUserCarKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersCars_AspNetUsers_UserId",
                table: "UsersCars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersCars",
                table: "UsersCars");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UsersCars",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UsersCars",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersCars",
                table: "UsersCars",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsersCars_UserId",
                table: "UsersCars",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersCars_AspNetUsers_UserId",
                table: "UsersCars",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersCars_AspNetUsers_UserId",
                table: "UsersCars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersCars",
                table: "UsersCars");

            migrationBuilder.DropIndex(
                name: "IX_UsersCars_UserId",
                table: "UsersCars");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsersCars");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UsersCars",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersCars",
                table: "UsersCars",
                columns: new[] { "UserId", "CarId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsersCars_AspNetUsers_UserId",
                table: "UsersCars",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
