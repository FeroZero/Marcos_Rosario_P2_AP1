using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Marcos_Rosario_P2_AP1.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ciudades",
                columns: table => new
                {
                    CiudadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Monto = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudades", x => x.CiudadId);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    CursoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    Asignatura = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Monto = table.Column<double>(type: "float", nullable: false),
                    Ciudadid = table.Column<int>(type: "int", nullable: false),
                    CiudadesCiudadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.CursoId);
                    table.ForeignKey(
                        name: "FK_Cursos_Ciudades_CiudadesCiudadId",
                        column: x => x.CiudadesCiudadId,
                        principalTable: "Ciudades",
                        principalColumn: "CiudadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CursosDetalle",
                columns: table => new
                {
                    DetalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursoId = table.Column<int>(type: "int", nullable: false),
                    CiudadId = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursosDetalle", x => x.DetalleId);
                    table.ForeignKey(
                        name: "FK_CursosDetalle_Ciudades_CiudadId",
                        column: x => x.CiudadId,
                        principalTable: "Ciudades",
                        principalColumn: "CiudadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursosDetalle_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "CursoId");
                });

            migrationBuilder.InsertData(
                table: "Ciudades",
                columns: new[] { "CiudadId", "Monto", "Nombre" },
                values: new object[,]
                {
                    { 1, 0.0, "Tenares" },
                    { 2, 0.0, "San Fracisco de Macoris" },
                    { 3, 0.0, "Santiago" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_CiudadesCiudadId",
                table: "Cursos",
                column: "CiudadesCiudadId");

            migrationBuilder.CreateIndex(
                name: "IX_CursosDetalle_CiudadId",
                table: "CursosDetalle",
                column: "CiudadId");

            migrationBuilder.CreateIndex(
                name: "IX_CursosDetalle_CursoId",
                table: "CursosDetalle",
                column: "CursoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CursosDetalle");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Ciudades");
        }
    }
}
