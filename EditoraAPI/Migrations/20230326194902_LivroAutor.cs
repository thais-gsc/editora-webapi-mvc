using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EditoraAPI.Migrations
{
    public partial class LivroAutor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "autores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sobrenome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_autores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "livros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ano = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_livros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AutorLivro",
                columns: table => new
                {
                    AutoresId = table.Column<int>(type: "int", nullable: false),
                    LivrosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutorLivro", x => new { x.AutoresId, x.LivrosId });
                    table.ForeignKey(
                        name: "FK_AutorLivro_autores_AutoresId",
                        column: x => x.AutoresId,
                        principalTable: "autores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutorLivro_livros_LivrosId",
                        column: x => x.LivrosId,
                        principalTable: "livros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutorLivro_LivrosId",
                table: "AutorLivro",
                column: "LivrosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutorLivro");

            migrationBuilder.DropTable(
                name: "autores");

            migrationBuilder.DropTable(
                name: "livros");
        }
    }
}
