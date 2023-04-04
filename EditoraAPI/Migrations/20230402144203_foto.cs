using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EditoraAPI.Migrations
{
    public partial class foto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capa",
                table: "livros");

            migrationBuilder.DropColumn(
                name: "Foto",
                table: "autores");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Capa",
                table: "livros",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Foto",
                table: "autores",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
