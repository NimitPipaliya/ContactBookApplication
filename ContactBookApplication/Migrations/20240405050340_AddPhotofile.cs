using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactBookApplication.Migrations
{
    public partial class AddPhotofile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "ContactBook",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "ContactBook");


        }
    }
}
