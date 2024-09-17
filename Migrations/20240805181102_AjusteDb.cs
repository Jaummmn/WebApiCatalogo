using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiCurso.Migrations
{
    /// <inheritdoc />
    public partial class AjusteDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoriaDescricao",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "CategoriaNome",
                table: "Produtos");

            migrationBuilder.AddColumn<string>(
                name: "CategoriaDescricao",
                table: "Categorias",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoriaDescricao",
                table: "Categorias");

            migrationBuilder.AddColumn<string>(
                name: "CategoriaDescricao",
                table: "Produtos",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoriaNome",
                table: "Produtos",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);
        }
    }
}
