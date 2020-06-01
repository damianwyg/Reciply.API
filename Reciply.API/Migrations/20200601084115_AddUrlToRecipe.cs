using Microsoft.EntityFrameworkCore.Migrations;

namespace Reciply.API.Migrations
{
    public partial class AddUrlToRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Recipes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Recipes");
        }
    }
}
