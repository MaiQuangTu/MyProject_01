using Microsoft.EntityFrameworkCore.Migrations;

namespace MyProject.Data.Migrations
{
    public partial class UpDateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreateUser",
                table: "Teacher",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifyUser",
                table: "Teacher",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreateUser",
                table: "Student",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifyUser",
                table: "Student",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreateUser",
                table: "Course",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifyUser",
                table: "Course",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateUser",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "ModifyUser",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "CreateUser",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "ModifyUser",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "CreateUser",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "ModifyUser",
                table: "Course");
        }
    }
}
