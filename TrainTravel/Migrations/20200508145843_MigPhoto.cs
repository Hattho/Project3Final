using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainTravel.Migrations
{
    public partial class MigPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "AspNetUsers");
        }
    }
}
