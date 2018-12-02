using Microsoft.EntityFrameworkCore.Migrations;

namespace ItMe.Migrations
{
    public partial class AddCvToLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CvId",
                table: "Languages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_CvId",
                table: "Languages",
                column: "CvId");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_Cvs_CvId",
                table: "Languages",
                column: "CvId",
                principalTable: "Cvs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Cvs_CvId",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Languages_CvId",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "CvId",
                table: "Languages");
        }
    }
}
