using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Minesweeper.Migrations
{
    public partial class GameState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameState",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameState",
                table: "Games");
        }
    }
}
