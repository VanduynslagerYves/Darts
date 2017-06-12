using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Darts.Data.Migrations
{
    public partial class initMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Speler",
                columns: table => new
                {
                    SpelerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumToegevoegd = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Naam = table.Column<string>(maxLength: 100, nullable: false),
                    Voornaam = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speler", x => x.SpelerId);
                });

            migrationBuilder.CreateTable(
                name: "Resultaat",
                columns: table => new
                {
                    ResultaatId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Punten = table.Column<int>(maxLength: 50, nullable: false),
                    SpeelDatum = table.Column<DateTime>(nullable: false),
                    SpelerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resultaat", x => x.ResultaatId);
                    table.ForeignKey(
                        name: "FK_Resultaat_Speler_SpelerId",
                        column: x => x.SpelerId,
                        principalTable: "Speler",
                        principalColumn: "SpelerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resultaat_SpelerId",
                table: "Resultaat",
                column: "SpelerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resultaat");

            migrationBuilder.DropTable(
                name: "Speler");
        }
    }
}
