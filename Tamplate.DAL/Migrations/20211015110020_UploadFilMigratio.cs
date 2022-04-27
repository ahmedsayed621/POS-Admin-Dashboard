using Microsoft.EntityFrameworkCore.Migrations;

namespace Tamplate.DAL.Migrations
{
    public partial class UploadFilMigratio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CvUrl",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CvUrl",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Employee");
        }
    }
}
