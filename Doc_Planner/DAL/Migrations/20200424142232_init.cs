using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Doc_Planner.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HDebutRdv = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Prenom = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Sexe = table.Column<bool>(type: "bit", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Diabete = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Poids = table.Column<double>(type: "float", nullable: false),
                    Taille = table.Column<double>(type: "float", nullable: false),
                    BMI = table.Column<double>(type: "float", nullable: false),
                    ExamenType = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    NISS = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointments", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointments");
        }
    }
}
