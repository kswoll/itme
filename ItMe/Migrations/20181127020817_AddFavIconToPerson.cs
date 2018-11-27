using Microsoft.EntityFrameworkCore.Migrations;

namespace ItMe.Migrations
{
    public partial class AddFavIconToPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FavIconS3Key",
                table: "Persons",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavIconS3Key",
                table: "Persons");
        }
    }
}
