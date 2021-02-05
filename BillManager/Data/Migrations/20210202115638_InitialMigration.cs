using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BillManager.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facturi",
                columns: table => new
                {
                    IdFactura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdLocatie = table.Column<int>(type: "int", nullable: false),
                    NumarFactura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataFactura = table.Column<DateTime>(type: "date", nullable: false),
                    NumeClient = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturi", x => new { x.IdFactura, x.IdLocatie });
                });

            migrationBuilder.CreateTable(
                name: "DetaliiFacturi",
                columns: table => new
                {
                    IdDetaliiFactura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdLocatie = table.Column<int>(type: "int", nullable: false),
                    NumeProdus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantitate = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    PretUnitar = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Valoare = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    IdFactura = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetaliiFacturi", x => new { x.IdDetaliiFactura, x.IdLocatie });
                    table.ForeignKey(
                        name: "FK_DetaliiFactura_Facturi",
                        columns: x => new { x.IdFactura, x.IdLocatie },
                        principalTable: "Facturi",
                        principalColumns: new[] { "IdFactura", "IdLocatie" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetaliiFacturi_IdFactura_IdLocatie",
                table: "DetaliiFacturi",
                columns: new[] { "IdFactura", "IdLocatie" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetaliiFacturi");

            migrationBuilder.DropTable(
                name: "Facturi");
        }
    }
}
