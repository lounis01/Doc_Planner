using Microsoft.EntityFrameworkCore.Migrations;

namespace Doc_Planner.Migrations
{
    public partial class hopiyref : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Emergency",
                table: "appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "HopitalDeRef",
                table: "appointments",
                type: "nvarchar(250)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Emergency",
                table: "appointments");

            migrationBuilder.DropColumn(
                name: "HopitalDeRef",
                table: "appointments");
        }
    }
}
