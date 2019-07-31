using Microsoft.EntityFrameworkCore.Migrations;

namespace Produto.WebApi.Migrations
{
    public partial class RemoverDescricaoCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Categoria");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Categoria",
                type: "varchar(500)",
                nullable: false,
                defaultValue: "");
        }
    }
}
