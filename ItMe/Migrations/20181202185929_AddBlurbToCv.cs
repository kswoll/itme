using Microsoft.EntityFrameworkCore.Migrations;

namespace ItMe.Migrations
{
    public partial class AddBlurbToCv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Blurb",
                table: "Cvs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Blurb",
                table: "Cvs");
        }
    }
}
