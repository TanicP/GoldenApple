using Microsoft.EntityFrameworkCore.Migrations;

namespace GoldenAppleApp.Migrations
{
    public partial class PropertyUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Propertys",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Propertys");
        }
    }
}
