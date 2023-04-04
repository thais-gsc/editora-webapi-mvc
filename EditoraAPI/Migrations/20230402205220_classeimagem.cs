using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EditoraAPI.Migrations
{
    public partial class classeimagem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CapaId",
                table: "livros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FotoId",
                table: "autores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Imagem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    NomeArquivo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagem", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_livros_CapaId",
                table: "livros",
                column: "CapaId");

            migrationBuilder.CreateIndex(
                name: "IX_autores_FotoId",
                table: "autores",
                column: "FotoId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_autores_Imagem_FotoId",
            //    table: "autores",
            //    column: "FotoId",
            //    principalTable: "Imagem",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_livros_Imagem_CapaId",
            //    table: "livros",
            //    column: "CapaId",
            //    principalTable: "Imagem",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_autores_Imagem_FotoId",
                table: "autores");

            migrationBuilder.DropForeignKey(
                name: "FK_livros_Imagem_CapaId",
                table: "livros");

            migrationBuilder.DropTable(
                name: "Imagem");

            migrationBuilder.DropIndex(
                name: "IX_livros_CapaId",
                table: "livros");

            migrationBuilder.DropIndex(
                name: "IX_autores_FotoId",
                table: "autores");

            migrationBuilder.DropColumn(
                name: "CapaId",
                table: "livros");

            migrationBuilder.DropColumn(
                name: "FotoId",
                table: "autores");
        }
    }
}
